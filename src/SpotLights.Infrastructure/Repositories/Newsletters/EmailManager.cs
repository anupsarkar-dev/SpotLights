using MailKit.Net.Smtp;
using MailKit.Security;
using Mapster;
using Microsoft.Extensions.Logging;
using MimeKit;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Infrastructure.Caches;
using SpotLights.Infrastructure.Repositories.Options;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Enums;
using System.Text.Json;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

public class EmailManager
{
    private readonly ILogger _logger;
    private readonly MarkdigProvider _markdigProvider;
    private readonly NewsletterProvider _newsletterProvider;
    private readonly OptionProvider _optionProvider;
    private readonly PostProvider _postProvider;
    private readonly SubscriberProvider _subscriberProvider;

    public EmailManager(
        ILogger<EmailManager> logger,
        MarkdigProvider markdigProvider,
        OptionProvider optionProvider,
        PostProvider postProvider,
        NewsletterProvider newsletterProvider,
        SubscriberProvider subscriberProvider
    )
    {
        _logger = logger;
        _markdigProvider = markdigProvider;
        _optionProvider = optionProvider;
        _postProvider = postProvider;
        _newsletterProvider = newsletterProvider;
        _subscriberProvider = subscriberProvider;
    }

    public async Task<MailSettingDto?> GetSettingsAsync()
    {
        string key = CacheKeys.BlogMailData;
        string? value = await _optionProvider.GetByValueAsync(key);
        if (value != null)
        {
            MailSettings? data = JsonSerializer.Deserialize<MailSettings>(value);
            return data.Adapt<MailSettingDto>();
        }
        return null;
    }

    public async Task PutSettingsAsync(MailSettingDto input)
    {
        string key = CacheKeys.BlogMailData;
        MailSettings data = input.Adapt<MailSettings>();
        string value = JsonSerializer.Serialize(data);
        await _optionProvider.SetValue(key, value);
    }

    public async Task<SendNewsletterState> SendNewsletter(int postId)
    {
        NewsletterDto? newsletter = await _newsletterProvider.FirstOrDefaultByPostIdAsync(postId);
        if (newsletter != null && newsletter.Success)
        {
            return SendNewsletterState.NewsletterSuccess;
        }

        PostDto post = await _postProvider.GetAsync(postId);
        if (post == null)
        {
            return SendNewsletterState.NotPost;
        }

        IEnumerable<SubscriberDto> subscribers = await _subscriberProvider.GetItemsAsync();
        if (!subscribers.Any())
        {
            return SendNewsletterState.NotSubscriber;
        }

        MailSettingDto? settings = await GetSettingsAsync();
        if (settings == null || !settings.Enabled)
        {
            return SendNewsletterState.NotMailEnabled;
        }

        string subject = post.Title;
        string content = _markdigProvider.ToHtml(post.Content);

        bool sent = await Send(settings, subscribers, subject, content);
        if (newsletter == null)
        {
            await _newsletterProvider.AddAsync(postId, sent);
        }
        else
        {
            await _newsletterProvider.UpdateAsync(newsletter.Id, sent);
        }
        return sent ? SendNewsletterState.OK : SendNewsletterState.SentError;
    }

    private SmtpClient GetClient(MailSettingDto settings)
    {
        try
        {
            SmtpClient client =
                new() { ServerCertificateValidationCallback = (s, c, h, e) => true };
            client.Connect(settings.Host, settings.Port, SecureSocketOptions.Auto);
            client.Authenticate(settings.UserEmail, settings.UserPassword);
            return client;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error connecting to SMTP client");
            throw;
        }
    }

    private async Task<bool> Send(
        MailSettingDto settings,
        IEnumerable<SubscriberDto> subscribers,
        string subject,
        string content
    )
    {
        SmtpClient client = GetClient(settings);
        if (client == null)
        {
            return false;
        }

        BodyBuilder bodyBuilder = new() { HtmlBody = content };

        foreach (SubscriberDto subscriber in subscribers)
        {
            try
            {
                MimeMessage message =
                    new() { Subject = subject, Body = bodyBuilder.ToMessageBody() };
                message.From.Add(new MailboxAddress(settings.FromName, settings.FromEmail));
                message.To.Add(new MailboxAddress(settings.ToName, subscriber.Email));
                _ = client.Send(message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    "Error sending email to {Email}: {Message}",
                    subscriber.Email,
                    ex.Message
                );
            }
        }
        client.Disconnect(true);
        return await Task.FromResult(true);
    }
}
