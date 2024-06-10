using System.Linq.Expressions;
using SpotLights.Common.Library.Base;

namespace SpotLights.Common.Library.Interfaces;

public interface IService<T>
  where T : BaseEntity
{
  Task<bool> AddAsync(T entity);
  Task<bool> AddRangeAsync(IEnumerable<T> entities);
  IQueryable<T> AsQuerable();
  Task<bool> DeleteAsync(IEnumerable<int>? ids);
  Task<bool> DeleteAsync(IEnumerable<T>? entities);
  Task<bool> DeleteAsync(int id);
  Task<bool> DeleteAsync(T entity);
  Task<IEnumerable<T>> GetAllAsync();
  Task<T?> GetByIdAsync(int id);
  Task<bool> UpdateAsync(T entity);
  Task<bool> UpdateRange(IEnumerable<T> entities);
}
