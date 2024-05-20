using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Domain.Base;
using SpotLights.Infrastructure.Interfaces;

namespace SpotLights.Infrastructure.Repositories;

internal class BaseContextRepository : IBaseContextRepository
{
    protected readonly ApplicationDbContext _context;

    protected BaseContextRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;

        // AsNoTracking by default
        //_context.ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public async Task AddAsync<T>(T entity)
        where T : BaseEntity
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task DeleteAsync<T>(T entity)
        where T : BaseEntity
    {
        await _context.Set<T>().Where(m => entity.Id.Equals(m.Id)).ExecuteDeleteAsync();
    }

    public async Task DeleteAsync<T>(DefaultIdType id)
        where T : BaseEntity
    {
        await _context.Set<T>().Where(m => id.Equals(m.Id)).ExecuteDeleteAsync();
    }

    public async Task DeleteAsync<T>(IEnumerable<T>? entities)
        where T : BaseEntity
    {
        if (entities != null && entities.Any())
        {
            await _context
                .Set<T>()
                .Where(m => entities.Select(s => s.Id).Contains(m.Id))
                .ExecuteDeleteAsync();
        }
    }

    public async Task DeleteAsync<T>(IEnumerable<DefaultIdType>? ids)
        where T : BaseEntity
    {
        if (ids != null && ids.Any())
        {
            await _context.Set<T>().Where(m => ids.Contains(m.Id)).ExecuteDeleteAsync();
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
