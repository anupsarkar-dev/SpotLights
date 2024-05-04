using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Enums;
using SpotLights.Shared.Extensions;
using System.Threading.Tasks;

namespace SpotLights.Controllers;

[Route("post")]
public class PostController : Controller
{
    protected readonly ILogger _logger;
    protected readonly MainManager _mainMamager;
    protected readonly PostManager _postManager;

    public PostController(
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
        PostViewModel model = new(postSlug, categoriesUrl, main);
        return View($"~/Views/Themes/{main.Theme}/post.cshtml", model);
    }
}
