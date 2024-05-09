using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Storage;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Infrastructure.Interfaces.Storages;
using SpotLights.Infrastructure.Repositories;
using SpotLights.Shared;
using SpotLights.Shared.Constants;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Manager.Storages;

public class StorageLocalProvider : BaseContextRepository, IStorageProvider
{
    private readonly ILogger _logger;
    private readonly string _pathLocalRoot;

    public StorageLocalProvider(
        ILogger<StorageLocalProvider> logger,
        ApplicationDbContext dbContext,
        IHostEnvironment hostEnvironment
    )
        : base(dbContext)
    {
        _logger = logger;
        _pathLocalRoot = Path.Combine(
            hostEnvironment.ContentRootPath,
            SpotLightsConstant.StorageLocalRoot
        );
    }

    public Task<bool> ExistsAsync(string slug)
    {
        throw new NotImplementedException();
    }

    public Task<StorageDto?> GetAsync(string slug, Func<Stream, CancellationToken, Task> callback)
    {
        throw new NotImplementedException();
    }

    public async Task<StorageDto?> GetCheckStoragAsync(string path)
    {
        IQueryable<Storage> query = _context.Storages.AsNoTracking().Where(m => m.Path == path);
        StorageDto? storage = await query.ProjectToType<StorageDto>().FirstOrDefaultAsync();
        bool existsing = Exists(path);
        if (storage == null)
        {
            if (existsing)
            {
                Delete(path);
            }
        }
        else
        {
            if (!existsing)
            {
                await DeleteAsync<Storage>(storage.Id);
                return null;
            }
        }
        return storage;
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
        Storage storage =
            new()
            {
                UploadAt = uploadAt,
                UserId = userid,
                Name = fileName,
                Path = path,
                Length = stream.Length,
                ContentType = contentType,
                Slug = await WriteAsync(path, stream),
                Type = StorageType.Local
            };
        await AddAsync(storage);
        return storage.Adapt<StorageDto>();
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
        Storage storage =
            new()
            {
                UploadAt = uploadAt,
                UserId = userid,
                Name = fileName,
                Path = path,
                Length = bytes.Length,
                ContentType = contentType,
                Slug = await WriteAsync(path, bytes),
                Type = StorageType.Local
            };
        await AddAsync(storage);
        return storage.Adapt<StorageDto>();
    }

    private void Delete(string path)
    {
        string storagePath = Path.Combine(_pathLocalRoot, path);
        _logger.LogInformation("file delete: {storagePath}", storagePath);
        File.Delete(storagePath);
    }

    private bool Exists(string path)
    {
        string storagePath = Path.Combine(_pathLocalRoot, path);
        _logger.LogInformation("file exists: {storagePath}", storagePath);
        return File.Exists(storagePath);
    }

    private async Task<string> WriteAsync(string path, Stream stream)
    {
        string storagePath = Path.Combine(_pathLocalRoot, path);
        string directoryPath = Path.GetDirectoryName(storagePath)!;
        if (!Directory.Exists(directoryPath))
        {
            _ = Directory.CreateDirectory(directoryPath);
        }

        using FileStream fileStream = new(storagePath, FileMode.CreateNew);
        await stream.CopyToAsync(fileStream);
        string virtualPath = $"{SpotLightsConstant.StorageLocalPhysicalRoot}/{path}";
        _logger.LogInformation(
            "file Write: {storagePath} => {virtualPath}",
            storagePath,
            virtualPath
        );
        return virtualPath;
    }

    private async Task<string> WriteAsync(string path, byte[] bytes)
    {
        string storagePath = Path.Combine(_pathLocalRoot, path);
        string directoryPath = Path.GetDirectoryName(storagePath)!;
        if (!Directory.Exists(directoryPath))
        {
            _ = Directory.CreateDirectory(directoryPath);
        }

        await File.WriteAllBytesAsync(storagePath, bytes);
        string virtualPath = $"{SpotLightsConstant.StorageLocalPhysicalRoot}/{path}";
        _logger.LogInformation(
            "file Write: {storagePath} => {virtualPath}",
            storagePath,
            virtualPath
        );
        return virtualPath;
    }
}
