using SpotLights.Domain.Model.Posts;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces.Posts
{
    internal interface ICategoryRepository : IBaseContextRepository
    {
        Task<Category> GetCategory(int categoryId);
        Task<List<CategoryItemDto>> GetItemsAsync();
        Task<List<CategoryItemDto>> GetAllAsync();
        Task<List<CategoryItemDto>> GetItemsExistPostAsync();
        Task<IEnumerable<Category>> GetPostCategories(int postId);
        Task<bool> SaveCategory(Category category);
        Task<Category> SaveCategory(string tag);
        Task<List<CategoryItemDto>> SearchCategories(string term);
        Task<bool> UpdateCategoryMenusStatusByIdAsync(int categoryId, bool status);
  }
}
