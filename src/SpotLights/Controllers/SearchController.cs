using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpotLights.Core.Interfaces.Blogs;
using SpotLights.Core.Interfaces.Post;

namespace SpotLights.Controllers;

[Route("search")]
internal class SearchController : Controller
{
    private readonly IMainService _mainMamager;
    private readonly IPostService _postProvider;

    public SearchController(IMainService mainMamager, IPostService postProvider)
    {
        _mainMamager = mainMamager;
        _postProvider = postProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] string? term = "", [FromForm] int page = 1)
    {
        if (!string.IsNullOrEmpty(term))
        {
            MainDto main = await _mainMamager.GetAsync();
            PostPagerDto pager = await _postProvider.GetSearchAsync(term, page, main.ItemsPerPage);
            pager.Configure(main.PathUrl, "page");
            SearchViewModel model = new(pager, main);
            return View($"~/Views/Themes/{main.Theme}/search.cshtml", model);
        }
        else
        {
            return Redirect("~/");
        }
    }

    //[HttpGet]
    //public async Task<IActionResult> Get([FromForm] string? term = "", [FromForm] int page = 1)
    //{
    //  if (!string.IsNullOrEmpty(term))
    //  {
    //    var main = await _mainMamager.GetAsync();
    //    var pager = await _postProvider.GetSearchAsync(term, page, main.ItemsPerPage);
    //    pager.Configure(main.PathUrl, "page");
    //    var model = new SearchModel(pager, main);
    //    return View($"~/Views/Themes/{main.Theme}/search.cshtml", model);
    //  }
    //  else
    //  {
    //    return Redirect("~/");
    //  }
    //}
}
