using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Interfaces.Newsletters
{
    public interface IEmailRepository
    {
        Task<MailSettingDto?> GetSettingsAsync();
        Task PutSettingsAsync(MailSettingDto input);
        Task<SendNewsletterState> SendNewsletter(int postId);
    }
}
