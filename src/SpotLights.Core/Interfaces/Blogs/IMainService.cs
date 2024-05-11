using SpotLights.Shared;

namespace SpotLights.Core.Interfaces.Blogs
{
    internal interface IMainService
    {
        Task<MainDto> GetAsync();
        Task<List<CategoryItemDto>> GetCategoryItemesAsync();
        Task<List<CategoryItemDto>> GetCategoryItemesCacheAsync();
    }
}
