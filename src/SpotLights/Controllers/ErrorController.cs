using AutoMapper;
using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using SpotLights.Data.Repositories.Blogs;

namespace SpotLights.Controllers;

public class ErrorController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly MainManager _mainMamager;

  public ErrorController(
    ILogger<ErrorController> logger,
    IMapper mapper,
    MainManager mainMamager)
  {
    _logger = logger;
    _mapper = mapper;
    _mainMamager = mainMamager;
  }

  [Route("404")]
  public async Task<IActionResult> Error404()
  {
    try
    {
      var data = await _mainMamager.GetAsync();
      var model = new MainModel(data);
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
      var data = await _mainMamager.GetAsync();
      var model = new MainModel(data);
      return View($"~/Views/Themes/{data.Theme}/404.cshtml", model);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "error page exception");
      return View($"~/Views/404.cshtml");
    }
  }
}
