using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Infrastructure.Repositories.Posts;

namespace SpotLights.Controllers;

[Route("category")]
public class CategoryController : Controller
{
    private readonly MainRepository _mainMamager;
    private readonly IPostService _postProvider;

    public CategoryController(MainRepository mainMamager, IPostService postProvider)
    {
        _mainMamager = mainMamager;
        _postProvider = postProvider;
    }

    [HttpGet("{category}")]
    public async Task<IActionResult> Category([FromRoute] string category, [FromQuery] int page = 1)
    {
        int userId = User.FirstUserId();
        bool isAdmin = User.IsAdmin();

        MainDto main = await _mainMamager.GetAsync();
        PostPagerDto pager = await _postProvider.GetByCategoryAsync(
            category,
            page,
            main.ItemsPerPage,
            userId,
            isAdmin
        );
        pager.Configure(main.PathUrl, "page");
        CategoryViewModel model = new(category, pager, main);
        return View($"~/Views/Themes/{main.Theme}/category.cshtml", model);
    }
}
