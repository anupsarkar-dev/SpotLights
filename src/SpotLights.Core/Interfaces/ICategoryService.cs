using SpotLights.Domain.Model.Posts;
using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategory(int categoryId);
        Task<List<CategoryItemDto>> GetItemsAsync();
        Task<List<CategoryItemDto>> GetItemsExistPostAsync();
        Task<IEnumerable<Category>> GetPostCategories(int postId);
        Task<bool> SaveCategory(Category category);
        Task<Category> SaveCategory(string tag);
        Task<List<CategoryItemDto>> SearchCategories(string term);
    }
}
