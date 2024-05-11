using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces.Blogs
{
    internal interface IMainRepository
    {
        Task<MainDto> GetAsync();
        Task<List<CategoryItemDto>> GetCategoryItemesAsync();
        Task<List<CategoryItemDto>> GetCategoryItemesCacheAsync();
    }
}
