using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    public interface INewsletterService
    {
        Task AddAsync(int postId, bool success);
        Task<NewsletterDto?> FirstOrDefaultByPostIdAsync(int postId);
        Task<IEnumerable<NewsletterDto>> GetItemsAsync(int userId, bool isAdmin);
        Task UpdateAsync(int id, bool success);
    }
}
