using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

public class NewsletterService : INewsletterService
{
    private readonly INewsletterRepository _newsletterRepository;

    public NewsletterService(INewsletterRepository newsletterRepository)
    {
        _newsletterRepository = newsletterRepository;
    }

    public async Task AddAsync(int postId, bool success)
    {
        await _newsletterRepository.AddAsync(postId, success);
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
