using SpotLights.Core.Services;
using SpotLights.Shared;

namespace SpotLights.Core.Interfaces.Newsletter
{
    public interface INewsletterService : IBaseContexService
    {
        Task AddAsync(int postId, bool success);
        Task<NewsletterDto?> FirstOrDefaultByPostIdAsync(int postId);
        Task<IEnumerable<NewsletterDto>> GetItemsAsync(int userId, bool isAdmin);
        Task UpdateAsync(int id, bool success);
    }
}
