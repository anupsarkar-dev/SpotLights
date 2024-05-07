using SpotLights.Shared.Extensions;
using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotLights.Infrastructure.Repositories.Newsletters;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Core.Interfaces.Newsletter;

namespace SpotLights.Interfaces;

[Route("api/newsletter")]
[ApiController]
[Authorize]
public class NewsletterController : ControllerBase
{
    private readonly INewsletterService _newsletterProvider;

    public NewsletterController(INewsletterService newsletterProvider)
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
        await _newsletterProvider.DeleteAsync<Newsletter>(id);
    }

    [HttpGet("send/{postId:int}")]
    public async Task SendNewsletter(
        [FromRoute] int postId,
        [FromServices] IEmailService emailManager
    )
    {
        await emailManager.SendNewsletter(postId);
    }
}
