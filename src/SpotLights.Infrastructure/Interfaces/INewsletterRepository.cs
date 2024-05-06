using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface INewsletterRepository
    {
        Task AddAsync(int postId, bool success);
        Task<NewsletterDto?> FirstOrDefaultByPostIdAsync(int postId);
        Task<IEnumerable<NewsletterDto>> GetItemsAsync(int userId, bool isAdmin);
        Task UpdateAsync(int id, bool success);
    }
}
