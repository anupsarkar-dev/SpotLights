
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using SpotLights.Caches;
using SpotLights.Data.Model.Blogs;
using SpotLights.Data.Repositories.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Identity;
using System.Text;
using System.Text.Json;

namespace SpotLights.Data.Repositories.Blogs;

public class MainManager
{
  private readonly IDistributedCache _distributedCache;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly BlogManager _blogManager;
  private readonly CategoryProvider _categoryProvider;

  public MainManager(
    IDistributedCache distributedCache,
    IHttpContextAccessor httpContextAccessor,
    BlogManager blogManager,
    CategoryProvider categoryProvider)
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
    main.Categories = await GetCategoryItemesAsync();
    HttpContext? httpContext = _httpContextAccessor.HttpContext;
    if (httpContext != null)
    {
      HttpRequest request = httpContext.Request;
      main.AbsoluteUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
      main.PathUrl = request.Path;
      main.Claims = SpotLightsClaims.Analysis(httpContext.User);
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
      await _distributedCache.SetAsync(key, bytes, new() { SlidingExpiration = TimeSpan.FromMinutes(15) });
      return data;
    }
  }

  public async Task<List<CategoryItemDto>> GetCategoryItemesAsync()
  {
    return await _categoryProvider.GetItemsExistPostAsync();
  }
}
