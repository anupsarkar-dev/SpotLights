using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using SpotLights.Domain.Dto;
using SpotLights.Infrastructure.Caches;
using SpotLights.Infrastructure.Interfaces.Blogs;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Infrastructure.Provider;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Entities.Identity;
using System.Text;
using System.Text.Json;

namespace SpotLights.Infrastructure.Repositories.Blogs;

internal class MainRepository : IMainRepository
{
    private readonly IDistributedCache _distributedCache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBlogDataProvider _blogManager;
    private readonly ICategoryRepository _categoryProvider;

    public MainRepository(
        IDistributedCache distributedCache,
        IHttpContextAccessor httpContextAccessor,
        IBlogDataProvider blogManager,
        ICategoryRepository categoryProvider
    )
    {
        _distributedCache = distributedCache;
        _httpContextAccessor = httpContextAccessor;
        _blogManager = blogManager;
        _categoryProvider = categoryProvider;
    }

    public async Task<MainDto> GetAsync()
    {
        BlogData blog = await _blogManager.GetAsync();
        MainDto main = blog.Adapt<MainDto>();
        var categories = await GetCategoryItemesAsync();
        main.Categories = categories.Where(c => c.IsShowInHomePage);
        HttpContext? httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            HttpRequest request = httpContext.Request;
            main.AbsoluteUrl =
                $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
            main.PathUrl = request.Path;
            main.Claims = IdentityClaims.Analysis(httpContext.User);
        }
        return main;
    }

    public async Task<List<CategoryItemDto>> GetCategoryItemesCacheAsync()
    {
        string key = CacheKeys.CategoryItemes;
        byte[]? cache = await _distributedCache.GetAsync(key);
        if (cache != null)
        {
            string value = Encoding.UTF8.GetString(cache);
            return JsonSerializer.Deserialize<List<CategoryItemDto>>(value)!;
        }
        else
        {
            List<CategoryItemDto> data = await GetCategoryItemesAsync();
            string value = JsonSerializer.Serialize(data);
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            await _distributedCache.SetAsync(
                key,
                bytes,
                new() { SlidingExpiration = TimeSpan.FromMinutes(15) }
            );
            return data;
        }
    }

    public async Task<List<CategoryItemDto>> GetCategoryItemesAsync()
    {
        return await _categoryProvider.GetItemsExistPostAsync();
    }
}
