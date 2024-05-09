using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using SpotLights.Domain.Dto;
using SpotLights.Infrastructure.Caches;
using SpotLights.Infrastructure.Interfaces.Blogs;
using SpotLights.Infrastructure.Interfaces.Options;
using SpotLights.Infrastructure.Repositories.Options;
using System.Text;
using System.Text.Json;

namespace SpotLights.Infrastructure.Provider;

public class BlogDataProvider : IBlogDataProvider
{
    private readonly ILogger _logger;
    private readonly IDistributedCache _distributedCache;
    private readonly IOptionRepository _optionProvider;
    private BlogData? _blogData;

    public BlogDataProvider(
        ILogger<BlogDataProvider> logger,
        IDistributedCache distributedCache,
        IOptionRepository optionProvider
    )
    {
        _logger = logger;
        _distributedCache = distributedCache;
        _optionProvider = optionProvider;
    }

    public async Task<BlogData> GetAsync()
    {
        if (_blogData != null)
        {
            return _blogData;
        }

        string key = CacheKeys.BlogData;
        _logger.LogDebug("get option {key}", key);
        byte[]? cache = await _distributedCache.GetAsync(key);
        if (cache != null)
        {
            string value = Encoding.UTF8.GetString(cache);
            return Deserialize(value);
        }
        else
        {
            string? value = await _optionProvider.GetByValueAsync(key);
            if (value != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                await _distributedCache.SetAsync(
                    key,
                    bytes,
                    new() { SlidingExpiration = TimeSpan.FromMinutes(15) }
                );
                return Deserialize(value);
            }
        }
        throw new BlogNotIitializeException();

        BlogData Deserialize(string value)
        {
            _logger.LogDebug("return option {key}:{value}", key, value);
            _blogData = JsonSerializer.Deserialize<BlogData>(value);
            return _blogData!;
        }
    }

    public async Task<bool> AnyAsync()
    {
        string key = CacheKeys.BlogData;
        if (await _optionProvider.AnyKeyAsync(key))
        {
            return true;
        }

        await _distributedCache.RemoveAsync(key);
        return false;
    }

    public async Task SetAsync(BlogData blogData)
    {
        string key = CacheKeys.BlogData;
        string value = JsonSerializer.Serialize(blogData);
        _logger.LogCritical("blog set {value}", value);
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        await _distributedCache.SetAsync(
            key,
            bytes,
            new() { SlidingExpiration = TimeSpan.FromMinutes(15) }
        );
        await _optionProvider.SetValue(key, value);
    }
}
