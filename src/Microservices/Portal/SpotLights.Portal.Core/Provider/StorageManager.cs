using SpotLights.Core.Interfaces.Provider;
using SpotLights.Infrastructure.Interfaces.Storages;
using SpotLights.Shared;

namespace SpotLights.Core.Provider
{
    internal class StorageManager : IStorageManager
    {
        private readonly IStorageMinioProvider _provider;

        public StorageManager(IStorageMinioProvider storageMinioProvider)
        {
            _provider = storageMinioProvider;
        }

        public async Task<StorageDto> AddAsync(
            DateTime uploadAt,
            int userid,
            string path,
            string fileName,
            Stream stream,
            string contentType
        )
        {
            return await _provider.AddAsync(uploadAt, userid, path, fileName, stream, contentType);
        }

        public async Task<StorageDto> AddAsync(
            DateTime uploadAt,
            int userid,
            string path,
            string fileName,
            byte[] bytes,
            string contentType
        )
        {
            return await _provider.AddAsync(uploadAt, userid, path, fileName, bytes, contentType);
        }

        public async Task<bool> ExistsAsync(string slug)
        {
            return await _provider.ExistsAsync(slug);
        }

        public async Task<StorageDto?> GetAsync(
            string slug,
            Func<Stream, CancellationToken, Task> callback
        )
        {
            return await _provider.GetAsync(slug, callback);
        }

        public async Task<StorageDto?> GetCheckStoragAsync(string path)
        {
            return await _provider.GetCheckStoragAsync(path);
        }
    }
}
