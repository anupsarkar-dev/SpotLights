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
public class MailController : ControllerBase
{
    private readonly IEmailService _emailManager;

    public MailController(IEmailService emailManager)
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
