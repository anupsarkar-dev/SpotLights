using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Core.Interfaces.Newsletter
{
    internal interface IEmailsService
    {
        Task<MailSettingDto?> GetSettingsAsync();
        Task PutSettingsAsync(MailSettingDto input);
        Task<SendNewsletterState> SendNewsletter(int postId);
    }
}
