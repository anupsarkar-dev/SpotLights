using SpotLights.Domain.Dto;

namespace SpotLights.Core.Interfaces
{
    public interface IBlogService
    {
        Task<bool> AnyAsync();
        Task<BlogData> GetAsync();
        Task SetAsync(BlogData blogData);
    }
}
