using SpotLights.Shared.Extensions;
using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotLights.Data.Newsletters;

namespace SpotLights.Interfaces;

[Route("api/newsletter")]
[ApiController]
[Authorize]
public class NewsletterController : ControllerBase
{
    private readonly NewsletterProvider _newsletterProvider;

    public NewsletterController(NewsletterProvider newsletterProvider)
    {
        _newsletterProvider = newsletterProvider;
    }

    [HttpGet("items")]
    public async Task<IEnumerable<NewsletterDto>> GetItemsAsync()
    {
    var userId = User.FirstUserId();
        var isAdmin = User.IsAdmin();

        return await _newsletterProvider.GetItemsAsync(userId, isAdmin);
    }

    [HttpDelete("{id:int}")]
    public async Task DeleteAsync([FromRoute] int id)
    {
        await _newsletterProvider.DeleteAsync(id);
    }

    [HttpGet("send/{postId:int}")]
    public async Task SendNewsletter(
        [FromRoute] int postId,
        [FromServices] EmailManager emailManager
    )
    {
        await emailManager.SendNewsletter(postId);
    }
}
