using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Infrastructure.Identity;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Domain.Model.Identity;
using SpotLights.Shared.Entities.Identity;
using SpotLights.Domain.Model.Blogs;
using SpotLights.Shared.Constants;

namespace SpotLights.Controllers;

[Route("account")]
public class AccountController : Controller
{
    protected readonly ILogger _logger;
    protected readonly UserManager _userManager;
    protected readonly SignInManager _signInManager;
    protected readonly BlogManager _blogManager;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager userManager,
        SignInManager signInManager,
        BlogManager blogManager
    )
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _blogManager = blogManager;
    }

    [HttpGet]
    [HttpPost]
    public IActionResult Index([FromQuery] AccountViewModel parameter) =>
        RedirectToAction("login", routeValues: parameter);

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] AccountViewModel parameter)
    {
        var data = await _blogManager.GetAsync();
        var model = new AccountLoginViewModel { RedirectUri = parameter.RedirectUri };
        return View($"~/Views/Themes/{data.Theme}/login.cshtml", model);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginForm([FromForm] AccountLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
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
        var data = await _blogManager.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/login.cshtml", model);
    }

    [HttpGet("register")]
    public async Task<IActionResult> Register([FromQuery] AccountViewModel parameter)
    {
        var model = new AccountRegisterViewModel { RedirectUri = parameter.RedirectUri };
        var data = await _blogManager.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/register.cshtml", model);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterForm([FromForm] AccountRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new UserInfo(model.UserName)
            {
                NickName = model.NickName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(
                    "login",
                    routeValues: new AccountViewModel { RedirectUri = model.RedirectUri }
                );
            }
        }
        model.ShowError = true;
        var data = await _blogManager.GetAsync();
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
        if (await _blogManager.AnyAsync())
            return RedirectToAction("login", routeValues: parameter);

        var model = new AccountInitializeViewModel { RedirectUri = parameter.RedirectUri };
        return View($"~/Views/Themes/{SpotLightsConstant.DefaultTheme}/initialize.cshtml", model);
    }

    [HttpPost("initialize")]
    public async Task<IActionResult> InitializeForm([FromForm] AccountInitializeViewModel model)
    {
        if (await _blogManager.AnyAsync())
            return RedirectToAction(
                "login",
                routeValues: new AccountViewModel { RedirectUri = model.RedirectUri }
            );

        if (ModelState.IsValid)
        {
            var user = new UserInfo(model.UserName)
            {
                NickName = model.NickName,
                Email = model.Email,
                Type = UserType.Administrator,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var blogData = new BlogData
                {
                    Title = model.Title,
                    Description = model.Description,
                    Theme = SpotLightsConstant.DefaultTheme,
                    ItemsPerPage = SpotLightsConstant.DefaultItemsPerPage,
                    Version = SpotLightsConstant.DefaultVersion,
                    Logo = SpotLightsSharedConstant.DefaultLogo
                };
                await _blogManager.SetAsync(blogData);
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
        var userId = User.FirstUserId();
        var user = await _userManager.FindByIdAsync(userId);
        var model = new AccountProfileEditViewModel
        {
            RedirectUri = parameter.RedirectUri,
            IsProfile = true,
            Email = user.Email,
            NickName = user.NickName,
            Avatar = user.Avatar,
            Bio = user.Bio,
        };
        var data = await _blogManager.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/profile.cshtml", model);
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<IActionResult> ProfileForm([FromForm] AccountProfileEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FirstUserId();
            var user = await _userManager.FindByIdAsync(userId);
            user.Email = model.Email;
            user.NickName = model.NickName;
            user.Avatar = model.Avatar;
            user.Bio = model.Bio;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
            }
            else
            {
                model.Error = result.Errors.FirstOrDefault()?.Description;
            }
        }
        var data = await _blogManager.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/profile.cshtml", model);
    }

    [Authorize]
    [HttpGet("password")]
    public async Task<IActionResult> Password([FromQuery] AccountViewModel parameter)
    {
        var model = new AccountProfilePasswordModel
        {
            RedirectUri = parameter.RedirectUri,
            IsPassword = true,
        };
        var data = await _blogManager.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/password.cshtml", model);
    }

    [Authorize]
    [HttpPost("password")]
    public async Task<IActionResult> Password([FromForm] AccountProfilePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FirstUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
            {
                return await Logout();
            }
            else
            {
                model.Error = result.Errors.FirstOrDefault()?.Description;
            }
        }
        var data = await _blogManager.GetAsync();
        return View($"~/Views/Themes/{data.Theme}/password.cshtml", model);
    }
}
