using SpotLights.Domain.Dto;

namespace SpotLights.Core.Interfaces
{
    internal interface IBlogService
    {
        Task<bool> AnyAsync();
        Task<BlogData> GetAsync();
        Task SetAsync(BlogData blogData);
    }
}
