using SpotLights.Core.Interfaces.Newsletter;
using SpotLights.Core.Services;
using SpotLights.Infrastructure.Interfaces.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

internal class NewsletterService : BaseContextService, INewsletterService
{
    private readonly INewsletterRepository _newsletterRepository;

    public NewsletterService(INewsletterRepository newsletterRepository)
        : base(newsletterRepository)
    {
        _newsletterRepository = newsletterRepository;
    }

    public async Task AddAsync(int postId, bool success)
    {
        await _newsletterRepository.AddNewsletterAsync(postId, success);
    }

    public async Task<NewsletterDto?> FirstOrDefaultByPostIdAsync(int postId)
    {
        return await _newsletterRepository.FirstOrDefaultByPostIdAsync(postId);
    }

    public async Task<IEnumerable<NewsletterDto>> GetItemsAsync(int userId, bool isAdmin)
    {
        return await _newsletterRepository.GetItemsAsync(userId, isAdmin);
    }

    public async Task UpdateAsync(int id, bool success)
    {
        await _newsletterRepository.UpdateAsync(id, success);
    }
}
