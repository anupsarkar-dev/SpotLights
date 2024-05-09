using SpotLights.Core.Interfaces.Newsletter;
using SpotLights.Infrastructure.Interfaces.Newsletters;
using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

public class EmailsService : IEmailsService
{
    private readonly IEmailManager _emailManager;

    public EmailsService(IEmailManager emailManager)
    {
        _emailManager = emailManager;
    }

    public async Task<MailSettingDto?> GetSettingsAsync()
    {
        return await _emailManager.GetSettingsAsync();
    }

    public async Task PutSettingsAsync(MailSettingDto input)
    {
        await _emailManager.PutSettingsAsync(input);
    }

    public async Task<SendNewsletterState> SendNewsletter(int postId)
    {
        return await _emailManager.SendNewsletter(postId);
    }
}
