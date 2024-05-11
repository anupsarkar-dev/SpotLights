using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpotLights.Infrastructure.Repositories.Newsletters;
using SpotLights.Core.Interfaces.Newsletter;

namespace SpotLights.Interfaces;

[Route("api/mail")]
[ApiController]
[Authorize]
internal class MailController : ControllerBase
{
    private readonly IEmailsService _emailManager;

    public MailController(IEmailsService emailManager)
    {
        _emailManager = emailManager;
    }

    [HttpGet("settings")]
    public async Task<MailSettingDto?> GetSettingsAsync()
    {
        return await _emailManager.GetSettingsAsync();
    }

    [HttpPut("settings")]
    public async Task PutSettingsAsync([FromBody] MailSettingDto input)
    {
        await _emailManager.PutSettingsAsync(input);
    }
}
