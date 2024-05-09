using SpotLights.Domain.Base;

namespace SpotLights.Core.Interfaces;

public interface IBaseService<T, TKey>
    where T : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public Task AddAsync(T entity);

    public Task DeleteAsync(TKey id);

    public Task DeleteAsync(IEnumerable<TKey>? ids);

    public Task DeleteInternalAsync(IQueryable<T> query);
}
