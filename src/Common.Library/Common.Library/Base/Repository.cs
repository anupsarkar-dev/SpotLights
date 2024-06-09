using Microsoft.EntityFrameworkCore;
using SpotLights.Common.Library.Interfaces;

namespace SpotLights.Common.Library.Base;

public class Repository<TContext> : IRepository
  where TContext : DbContext
{
  protected readonly TContext _context;

  public Repository(TContext context)
  {
    _context = context;
  }

  public async Task<T?> GetByIdAsync<T>(int id)
    where T : BaseEntity
  {
    return await _context.Set<T>().FindAsync(id);
  }

  public async Task<IEnumerable<T>> GetAllAsync<T>()
    where T : BaseEntity
  {
    return await _context.Set<T>().ToListAsync();
  }

  public IQueryable<T> AsQuerable<T>()
    where T : BaseEntity
  {
    return _context.Set<T>();
  }

  public async Task AddAsync<T>(T entity)
    where T : BaseEntity
  {
    await _context.Set<T>().AddAsync(entity);
  }

  public async Task AddRangeAsync<T>(IEnumerable<T> entities)
    where T : BaseEntity
  {
    await _context.Set<T>().AddRangeAsync(entities);
  }

  public async Task DeleteAsync<T>(T entity)
    where T : BaseEntity
  {
    await _context.Set<T>().Where(m => entity.Id.Equals(m.Id)).ExecuteDeleteAsync();
  }

  public void Update<T>(T entity)
    where T : BaseEntity
  {
    _context.Set<T>().Update(entity);
  }

  public void UpdateRange<T>(IEnumerable<T> entities)
    where T : BaseEntity
  {
    _context.Set<T>().UpdateRange(entities);
  }

  public async Task DeleteAsync<T>(int id)
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

  public async Task DeleteAsync<T>(IEnumerable<int>? ids)
    where T : BaseEntity
  {
    if (ids != null && ids.Any())
    {
      await _context.Set<T>().Where(m => ids.Contains(m.Id)).ExecuteDeleteAsync();
    }
  }

  public async Task<bool> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync() > 0;
  }
}
