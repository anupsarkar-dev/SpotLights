using SpotLights.Core.Interfaces;
using SpotLights.Domain.Dto;
using SpotLights.Infrastructure.Interfaces.Blogs;

namespace SpotLights.Infrastructure.Repositories.Blogs;

public class BlogService : IBlogService
{
    private readonly IBlogDataProvider _blogRepository;

    public BlogService(IBlogDataProvider blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<bool> AnyAsync()
    {
        return await _blogRepository.AnyAsync();
    }

    public Task<BlogData> GetAsync()
    {
        return _blogRepository.GetAsync();
    }

    public Task SetAsync(BlogData blogData)
    {
        return _blogRepository.SetAsync(blogData);
    }
}
