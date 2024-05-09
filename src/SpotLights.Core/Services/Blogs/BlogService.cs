using SpotLights.Core.Interfaces;
using SpotLights.Domain.Dto;
using SpotLights.Infrastructure.Interfaces.Blogs;

namespace SpotLights.Core.Services.Blogs;

public class BlogService : IBlogService
{
    private readonly IBlogDataProvider _repo;

    public BlogService(IBlogDataProvider repo)
    {
        _repo = repo;
    }

    public async Task<bool> AnyAsync()
    {
        return await _repo.AnyAsync();
    }

    public Task<BlogData> GetAsync()
    {
        return _repo.GetAsync();
    }

    public Task SetAsync(BlogData blogData)
    {
        return _repo.SetAsync(blogData);
    }
}
