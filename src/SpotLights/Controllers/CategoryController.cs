using SpotLights.Blogs;
using SpotLights.Posts;
using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;

namespace SpotLights.Controllers;

[Route("category")]
public class CategoryController : Controller
{
    private readonly MainMamager _mainMamager;
    private readonly PostProvider _postProvider;

    public CategoryController(MainMamager mainMamager, PostProvider postProvider)
    {
        _mainMamager = mainMamager;
        _postProvider = postProvider;
    }

    [HttpGet("{category}")]
    public async Task<IActionResult> Category([FromRoute] string category, [FromQuery] int page = 1)
    {
        var userId = User.FirstUserId();
        var isAdmin = User.IsAdmin();

        var main = await _mainMamager.GetAsync();
        var pager = await _postProvider.GetByCategoryAsync(
            category,
            page,
            main.ItemsPerPage,
            userId,
            isAdmin
        );
        pager.Configure(main.PathUrl, "page");
        var model = new CategoryModel(category, pager, main);
        return View($"~/Views/Themes/{main.Theme}/category.cshtml", model);
    }
}
