using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotLights.Core.Interfaces;
using SpotLights.Core.Interfaces.Blogs;
using SpotLights.Shared;
using SpotLights.Shared.Enums;
using SpotLights.Shared.Extensions;
using System.Threading.Tasks;

namespace SpotLights.Controllers;

[Route("post")]
public class PostController : Controller
{
    protected readonly ILogger _logger;
    protected readonly IMainService _mainMamager;
    protected readonly IPostProviderService _postManager;

    public PostController(
        ILogger<PostController> logger,
        IMainService mainMamager,
        IPostProviderService postManager
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
