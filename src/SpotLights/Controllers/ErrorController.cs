using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Shared;
using System;
using System.Threading.Tasks;

namespace SpotLights.Controllers;

public class ErrorController : Controller
{
    protected readonly ILogger _logger;
    protected readonly MainManager _mainMamager;

    public ErrorController(ILogger<ErrorController> logger, MainManager mainMamager)
    {
        _logger = logger;
        _mainMamager = mainMamager;
    }

    [Route("404")]
    public async Task<IActionResult> Error404()
    {
        try
        {
            MainDto data = await _mainMamager.GetAsync();
            MainViewModel model = new(data);
            return View($"~/Views/Themes/{data.Theme}/404.cshtml", model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error page exception");
            return View($"~/Views/404.cshtml");
        }
    }

    [Route("404")]
    public async Task<IActionResult> Error4041()
    {
        try
        {
            MainDto data = await _mainMamager.GetAsync();
            MainViewModel model = new(data);
            return View($"~/Views/Themes/{data.Theme}/404.cshtml", model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error page exception");
            return View($"~/Views/404.cshtml");
        }
    }
}
