using SpotLights.Domain.Base;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface IBaseContextRepository
    {
        Task AddAsync<T>(T entity)
            where T : BaseEntity;
        Task DeleteAsync<T>(IEnumerable<T>? entities)
            where T : BaseEntity;
        Task DeleteAsync<T>(T entity)
            where T : BaseEntity;
        Task DeleteAsync<T>(IEnumerable<int>? ids)
            where T : BaseEntity;
        Task DeleteAsync<T>(int id)
            where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
