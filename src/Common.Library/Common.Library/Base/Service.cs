using System.Linq.Expressions;
using SpotLights.Common.Library.Interfaces;

namespace SpotLights.Common.Library.Base
{
  public class Service<T> : IService<T>
    where T : BaseEntity
  {
    private readonly IRepository _repository;

    public Service(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
      return await _repository.GetAllAsync<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
      return await _repository.GetByIdAsync<T>(id);
    }

    public async Task<bool> AddAsync(T entity)
    {
      await _repository.AddAsync(entity);
      return await _repository.SaveChangesAsync();
    }

    public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
      await _repository.AddRangeAsync(entities);
      return await _repository.SaveChangesAsync();
    }

    public IQueryable<T> AsQuerable()
    {
      return _repository.AsQuerable<T>();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
      _repository.Update(entity);
      return await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateRange(IEnumerable<T> entities)
    {
      _repository.UpdateRange(entities);
      return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(T entity)
    {
      await _repository.DeleteAsync(entity);
      return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(IEnumerable<T>? entities)
    {
      await _repository.DeleteAsync(entities);
      return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(IEnumerable<int>? ids)
    {
      await _repository.DeleteAsync<T>(ids);
      return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
      await _repository.DeleteAsync<T>(id);
      return await _repository.SaveChangesAsync();
    }
  }
}
