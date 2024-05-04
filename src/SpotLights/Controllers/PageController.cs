using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotLights.Data.Repositories.Blogs;
using SpotLights.Data.Repositories.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Extensions;
using System.Threading.Tasks;

namespace SpotLights.Controllers;

[Route("page")]
public class PageController : Controller
{
    protected readonly ILogger _logger;
    protected readonly MainManager _mainMamager;
    protected readonly PostManager _postManager;

    public PageController(
        ILogger<PageController> logger,
        MainManager mainMamager,
        PostManager postManager
    )
    {
        _logger = logger;
        _mainMamager = mainMamager;
        _postManager = postManager;
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetAsync([FromRoute] string slug)
    {
        MainDto main = await _mainMamager.GetAsync();
        PostSlugDto postSlug = await _postManager.GetToHtmlAsync(slug);
        if (postSlug.Post.State == PostState.Draft)
        {
            if (User.Identity == null || User.FirstUserId() != postSlug.Post.User.Id)
            {
                return Redirect("~/404");
            }
        }
        else if (postSlug.Post.PostType == PostType.Page)
        {
            return Redirect($"~/page/{postSlug.Post.Slug}");
        }
        string categoriesUrl = Url.Content("~/category");
        PostModel model = new(postSlug, categoriesUrl, main);
        return View($"~/Views/Themes/{main.Theme}/post.cshtml", model);
    }
}
