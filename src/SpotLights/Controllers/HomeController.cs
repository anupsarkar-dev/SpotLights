using SpotLights.Models;
using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Domain.Dto;

namespace SpotLights.Controllers;

public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly MainRepository _mainMamager;
    private readonly PostRepository _postProvider;

    public HomeController(
        ILogger<HomeController> logger,
        MainRepository mainMamager,
        PostRepository postProvider
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
        var pager = await _postProvider.GetPostsAsync(page, main.ItemsPerPage);
        pager.Configure(main.PathUrl, "page");
        var model = new IndexViewModel(pager, main);
        return View($"~/Views/Themes/{main.Theme}/index.cshtml", model);
    }
}
