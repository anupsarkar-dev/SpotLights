using SpotLights.Core.Interfaces;
using SpotLights.Domain.Model.Posts;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Shared;

namespace SpotLights.Core.Services.Posts;

internal class CategoryService : BaseContextService, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
        : base(categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> GetCategory(int categoryId)
    {
        return await _categoryRepository.GetCategory(categoryId);
    }

    public async Task<List<CategoryItemDto>> GetItemsAsync()
    {
        return await _categoryRepository.GetItemsAsync();
    }

    public async Task<List<CategoryItemDto>> GetItemsExistPostAsync()
    {
        return await _categoryRepository.GetItemsExistPostAsync();
    }

    public async Task<IEnumerable<Category>> GetPostCategories(int postId)
    {
        return await _categoryRepository.GetPostCategories(postId);
    }

    public async Task<bool> SaveCategory(Category category)
    {
        return await _categoryRepository.SaveCategory(category);
    }

    public async Task<Category> SaveCategory(string tag)
    {
        return await _categoryRepository.SaveCategory(tag);
    }

    public async Task<List<CategoryItemDto>> SearchCategories(string term)
    {
        return await _categoryRepository.SearchCategories(term);
    }

    public async Task<bool> UpdateCategoryMenusStatusByIdAsync(int categoryId, bool status)
    {
      return await _categoryRepository.UpdateCategoryMenusStatusByIdAsync(categoryId, status);
    }
}
