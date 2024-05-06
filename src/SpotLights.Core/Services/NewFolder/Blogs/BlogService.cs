using SpotLights.Core.Interfaces;
using SpotLights.Domain.Model.Blogs;
using SpotLights.Infrastructure.Interfaces;

namespace SpotLights.Infrastructure.Repositories.Blogs;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    public BlogService(IBlogRepository blogRepository)
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
