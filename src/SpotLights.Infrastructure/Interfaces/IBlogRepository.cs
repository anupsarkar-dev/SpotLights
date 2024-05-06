using SpotLights.Domain.Model.Blogs;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface IBlogRepository
    {
        Task<bool> AnyAsync();
        Task<BlogData> GetAsync();
        Task SetAsync(BlogData blogData);
    }
}
