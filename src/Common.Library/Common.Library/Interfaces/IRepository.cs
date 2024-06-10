using System.Linq.Expressions;
using SpotLights.Common.Library.Base;

namespace SpotLights.Common.Library.Interfaces
{
  public interface IRepository
  {
    Task AddAsync<T>(T entity)
      where T : BaseEntity;
    Task AddRangeAsync<T>(IEnumerable<T> entities)
      where T : BaseEntity;
    IQueryable<T> AsQuerable<T>()
      where T : BaseEntity;
    Task DeleteAsync<T>(IEnumerable<int>? ids)
      where T : BaseEntity;
    Task DeleteAsync<T>(IEnumerable<T>? entities)
      where T : BaseEntity;
    Task DeleteAsync<T>(int id)
      where T : BaseEntity;
    Task DeleteAsync<T>(T entity)
      where T : BaseEntity;
    Task<IEnumerable<T>> GetAllAsync<T>()
      where T : BaseEntity;

    Task<T?> GetByIdAsync<T>(int id)
      where T : BaseEntity;
    Task<bool> SaveChangesAsync();
    void Update<T>(T entity)
      where T : BaseEntity;
    void UpdateRange<T>(IEnumerable<T> entities)
      where T : BaseEntity;
  }
}
