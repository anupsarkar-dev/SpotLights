using Microsoft.EntityFrameworkCore;
using SpotLights.Domain;

namespace SpotLights.Data.Data;

public class AppProvider<T, TKey>
    where T : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly AppDbContext _dbContext;

    protected AppProvider(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected async Task AddAsync(T entity)
    {
        _ = _dbContext.Set<T>().Add(entity);
        _ = await _dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(TKey id)
    {
        IQueryable<T> query = _dbContext.Set<T>().Where(m => id.Equals(m.Id));
        return DeleteInternalAsync(query);
    }

    public Task DeleteAsync(IEnumerable<TKey>? ids)
    {
        if (ids != null && ids.Any())
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(m => ids.Contains(m.Id));
            return DeleteInternalAsync(query);
        }
        return Task.CompletedTask;
    }

    protected static async Task DeleteInternalAsync(IQueryable<T> query)
    {
        _ = await query.ExecuteDeleteAsync();
    }
}
