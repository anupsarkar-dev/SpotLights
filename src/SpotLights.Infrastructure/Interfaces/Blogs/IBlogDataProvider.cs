using SpotLights.Domain.Dto;

namespace SpotLights.Infrastructure.Interfaces.Blogs
{
    public interface IBlogDataProvider
    {
        Task<bool> AnyAsync();
        Task<BlogData> GetAsync();
        Task SetAsync(BlogData blogData);
    }
}
