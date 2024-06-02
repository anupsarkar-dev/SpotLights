using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Domain.Model.Identity;
using SpotLights.Shared.Entities.Identity;
using SpotLights.Shared.Constants;
using SpotLights.Domain.Dto;
using SpotLights.Core.Interfaces;
using SpotLights.Core.Interfaces.Provider;
using SpotLights.Infrastructure.Identity;

namespace SpotLights.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly ILogger _logger;
    private readonly IIdentityService _identityProvider;
    private readonly IBlogService _blogService;
    private readonly UserManager _userManager;
    private readonly SignInManager _signInManager;

    public AccountController(
        ILogger<AccountController> logger,
        IBlogService blogManager,
        IIdentityService identityProvider,
        UserManager userManager,
        SignInManager signInManager
    )
    {
        _logger = logger;
        _blogService = blogManager;
        _identityProvider = identityProvider;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    [HttpPost]
    public IActionResult Index([FromQuery] AccountViewModel parameter) =>
        RedirectToAction("login", routeValues: parameter);

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] AccountViewModel parameter)
    {
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        AccountLoginViewModel model = new() { RedirectUri = parameter.RedirectUri };
        return View($"~/Views/Themes/{data.Theme}/login.cshtml", model);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginForm([FromForm] AccountLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            UserInfo? user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(
                        user,
                        model.Password,
                        true,
                        lockoutOnFailure: true
                    );
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    if (string.IsNullOrEmpty(model.RedirectUri))
                        return LocalRedirect("~/");
                    return Redirect(model.RedirectUri);
                }
            }
        }
        model.ShowError = true;
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/login.cshtml", model);
    }

    [HttpGet("register")]
    public async Task<IActionResult> Register([FromQuery] AccountViewModel parameter)
    {
        AccountRegisterViewModel model = new() { RedirectUri = parameter.RedirectUri };
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/register.cshtml", model);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterForm([FromForm] AccountRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            UserInfo user = new(model.UserName) { NickName = model.NickName, Email = model.Email };
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.CreateAsync(
                user,
                model.Password
            );
            if (result.Succeeded)
            {
                return RedirectToAction(
                    "login",
                    routeValues: new AccountViewModel { RedirectUri = model.RedirectUri }
                );
            }
        }
        model.ShowError = true;
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/register.cshtml", model);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("~/");
    }

    [HttpGet("initialize")]
    public async Task<IActionResult> Initialize([FromQuery] AccountViewModel parameter)
    {
        if (await _blogService.AnyAsync())
            return RedirectToAction("login", routeValues: parameter);

        AccountInitializeViewModel model = new() { RedirectUri = parameter.RedirectUri };
        return View($"~/Views/Themes/{SpotLightsConstant.DefaultTheme}/initialize.cshtml", model);
    }

    [HttpPost("initialize")]
    public async Task<IActionResult> InitializeForm([FromForm] AccountInitializeViewModel model)
    {
        if (await _blogService.AnyAsync())
            return RedirectToAction(
                "login",
                routeValues: new AccountViewModel { RedirectUri = model.RedirectUri }
            );

        if (ModelState.IsValid)
        {
            UserInfo user =
                new(model.UserName)
                {
                    NickName = model.NickName,
                    Email = model.Email,
                    Type = UserType.Administrator,
                };
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.CreateAsync(
                user,
                model.Password
            );
            if (result.Succeeded)
            {
                BlogData blogData =
                    new()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Theme = SpotLightsConstant.DefaultTheme,
                        ItemsPerPage = SpotLightsConstant.DefaultItemsPerPage,
                        Version = SpotLightsConstant.DefaultVersion,
                        Logo = SpotLightsSharedConstant.DefaultLogo
                    };
                await _blogService.SetAsync(blogData);
                return Redirect("~/");
            }
        }
        model.ShowError = true;
        return View($"~/Views/Themes/{SpotLightsConstant.DefaultTheme}/initialize.cshtml", model);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile([FromQuery] AccountViewModel parameter)
    {
        DefaultIdType userId = User.FirstUserId();
        UserInfo? user = await _identityProvider.FindByIdAsync(userId);

        AccountProfileEditViewModel model =
            new()
            {
                RedirectUri = parameter.RedirectUri,
                IsProfile = true,
                Email = user!.Email,
                NickName = user.NickName,
                Avatar = user.Avatar,
                Bio = user.Bio,
            };
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/profile.cshtml", model);
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<IActionResult> ProfileForm([FromForm] AccountProfileEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            int userId = User.FirstUserId();
            UserInfo user = await _identityProvider.FindByIdAsync(userId);
            user.Email = model.Email;
            user.NickName = model.NickName;
            user.Avatar = model.Avatar;
            user.Bio = model.Bio;
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.UpdateAsync(
                user
            );
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
            }
            else
            {
                model.Error = result.Errors.FirstOrDefault()?.Description;
            }
        }
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/profile.cshtml", model);
    }

    [Authorize]
    [HttpGet("password")]
    public async Task<IActionResult> Password([FromQuery] AccountViewModel parameter)
    {
        AccountProfilePasswordModel model =
            new() { RedirectUri = parameter.RedirectUri, IsPassword = true, };
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/password.cshtml", model);
    }

    [Authorize]
    [HttpPost("password")]
    public async Task<IActionResult> Password([FromForm] AccountProfilePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            int userId = User.FirstUserId();
            UserInfo user = await _identityProvider.FindByIdAsync(userId);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            Microsoft.AspNetCore.Identity.IdentityResult result =
                await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
            {
                return await Logout();
            }
            else
            {
                model.Error = result.Errors.FirstOrDefault()?.Description;
            }
        }
        Domain.Dto.BlogData data = await _blogService.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/password.cshtml", model);
    }
}
