using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces.Newsletters;
using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;

    public EmailService(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }

    public async Task<MailSettingDto?> GetSettingsAsync()
    {
        return await _emailRepository.GetSettingsAsync();
    }

    public async Task PutSettingsAsync(MailSettingDto input)
    {
        await _emailRepository.PutSettingsAsync(input);
    }

    public async Task<SendNewsletterState> SendNewsletter(int postId)
    {
        return await _emailRepository.SendNewsletter(postId);
    }
}
