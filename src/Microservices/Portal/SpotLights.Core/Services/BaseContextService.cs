using SpotLights.Domain.Base;
using SpotLights.Infrastructure.Interfaces;

namespace SpotLights.Core.Services
{
    internal class BaseContextService : IBaseContexService
    {
        private readonly IBaseContextRepository _repository;

        public BaseContextService(IBaseContextRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync<T>(T entity)
            where T : BaseEntity
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync<T>(IEnumerable<T>? entities)
            where T : BaseEntity
        {
            await _repository.DeleteAsync(entities);
        }

        public async Task DeleteAsync<T>(T entity)
            where T : BaseEntity
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task DeleteAsync<T>(IEnumerable<DefaultIdType>? ids)
            where T : BaseEntity
        {
            await _repository.DeleteAsync<T>(ids);
        }

        public async Task DeleteAsync<T>(DefaultIdType id)
            where T : BaseEntity
        {
            await _repository.DeleteAsync<T>(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }
    }
}
