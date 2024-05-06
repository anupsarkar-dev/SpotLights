using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using SpotLights.Data.Data;
using SpotLights.Domain.Dto;
using SpotLights.Infrastructure.Interfaces.Options;

namespace SpotLights.Infrastructure.Repositories.Options;

public class OptionRepository : IOptionRepository
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;

    public OptionRepository(
        ILogger<OptionRepository> logger,
        IDistributedCache distributedCache,
        ApplicationDbContext dbContext
    )
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<bool> AnyKeyAsync(string key)
    {
        return await _dbContext.Options.AnyAsync(m => m.Key == key);
    }

    public async Task<string?> GetByValueAsync(string key)
    {
        return await _dbContext.Options
            .AsNoTracking()
            .Where(m => m.Key == key)
            .Select(m => m.Value)
            .FirstOrDefaultAsync();
    }

    public async Task SetValue(string key, string value)
    {
        OptionInfo? option = await _dbContext.Options
            .Where(m => m.Key == key)
            .FirstOrDefaultAsync();
        if (option == null)
        {
            _dbContext.Options.Add(new OptionInfo { Key = key, Value = value });
        }
        else
        {
            option.Value = value;
        }
        _ = await _dbContext.SaveChangesAsync();
    }
}
