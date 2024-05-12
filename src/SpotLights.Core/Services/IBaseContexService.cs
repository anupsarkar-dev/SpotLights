using SpotLights.Domain.Base;

namespace SpotLights.Core.Services
{
    public interface IBaseContexService
    {
        Task AddAsync<T>(T entity)
            where T : BaseEntity;
        Task DeleteAsync<T>(IEnumerable<T>? entities)
            where T : BaseEntity;
        Task DeleteAsync<T>(T entity)
            where T : BaseEntity;
        Task DeleteAsync<T>(IEnumerable<DefaultIdType>? ids)
            where T : BaseEntity;
        Task DeleteAsync<T>(DefaultIdType id)
            where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
