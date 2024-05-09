using SpotLights.Models;
using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SpotLights.Domain.Dto;
using SpotLights.Core.Interfaces.Blogs;
using SpotLights.Core.Interfaces.Post;

namespace SpotLights.Controllers;

public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IMainService _mainMamager;
    private readonly IPostService _postProvider;

    public HomeController(
        ILogger<HomeController> logger,
        IMainService mainMamager,
        IPostService postProvider
    )
    {
        _logger = logger;
        _mainMamager = mainMamager;
        _postProvider = postProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int page = 1)
    {
        MainDto main;
        try
        {
            main = await _mainMamager.GetAsync();
        }
        catch (BlogNotIitializeException ex)
        {
            _logger.LogError(ex, "blgo not iitialize redirect");
            return Redirect("~/account/initialize");
        }
        PostPagerDto pager = await _postProvider.GetPostsAsync(page, main.ItemsPerPage);
        pager.Configure(main.PathUrl, "page");
        IndexViewModel model = new(pager, main);
        return View($"~/Views/Themes/{main.Theme}/index.cshtml", model);
    }
}
