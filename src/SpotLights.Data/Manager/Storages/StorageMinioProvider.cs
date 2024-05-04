using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using SpotLights.Shared;
using Mapster;

namespace SpotLights.Data.Manager.Storages;

public class StorageMinioProvider : AppProvider<Storage, int>, IStorageProvider, IDisposable
{
  private readonly ILogger _logger;
  private readonly string _bucketName;
  private readonly MinioClient _minioClient;

  public StorageMinioProvider(
    ILogger<StorageMinioProvider> logger,
    AppDbContext dbContext,
    IHttpClientFactory httpClientFactory,
    IConfigurationSection section) : base(dbContext)
  {
    _logger = logger;
    _bucketName = section.GetValue<string>("BucketName")!;
    _minioClient = (MinioClient?)new MinioClient()
     .WithEndpoint(section.GetValue<string>("Endpoint")!, section.GetValue<int>("Port"))
     .WithRegion(section.GetValue<string>("Region")!)
     .WithCredentials(section.GetValue<string>("AccessKey")!, section.GetValue<string>("SecretKey")!)
     .WithHttpClient(httpClientFactory.CreateClient())
     .Build();
  }

  // 判断 minio 中文件是否存在
  public Task<bool> ExistsAsync(string slug)
  {
    throw new NotImplementedException();
  }

  public async Task<StorageDto?> GetAsync(string slug, Func<Stream, CancellationToken, Task> callback)
  {
    _logger.LogInformation("Storage slug:{slug}", slug);
    Storage? storage = await _dbContext.Storages.FirstOrDefaultAsync(m => m.Slug == slug);
    if (storage == null)
    {
      return null;
    }

    ObjectStat objectStat = await GetObjectAsync(slug, callback);
    if (objectStat == null)
    {
      return null;
    }

    storage.ContentType = objectStat.ContentType;
    storage.Length = objectStat.Size;
    return storage.Adapt<StorageDto>();
  }

  public async Task<StorageDto?> GetCheckStoragAsync(string path)
  {
    IQueryable<Storage> query = _dbContext.Storages.AsNoTracking().Where(m => m.Path == path);
    var storage = await query.ProjectToType<StorageDto>().FirstOrDefaultAsync();
    throw new NotImplementedException();
  }

  // 想 minio中添加文件
  public async Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, Stream stream, string contentType)
  {
    PutObjectArgs args = new PutObjectArgs()
      .WithBucket(_bucketName)
      .WithObject(path)
      .WithStreamData(stream)
      .WithContentType(contentType);
    Minio.DataModel.Response.PutObjectResponse result = await _minioClient.PutObjectAsync(args).ConfigureAwait(false);
    Storage storage = new()
    {
      UploadAt = uploadAt,
      UserId = userid,
      Name = fileName,
      Path = path,
      Length = result.Size,
      ContentType = contentType,
      Slug = result.ObjectName,
      Type = StorageType.Minio
    };
    await AddAsync(storage);
    return storage.Adapt<StorageDto>();
  }

  public Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, byte[] bytes, string contentType)
  {
    using MemoryStream stream = new(bytes);
    return AddAsync(uploadAt, userid, path, fileName, stream, contentType);
  }

  private async Task<ObjectStat> GetObjectAsync(string objectName, Func<Stream, CancellationToken, Task> callback)
  {
    GetObjectArgs args = new GetObjectArgs().WithBucket(_bucketName).WithObject(objectName).WithCallbackStream(callback);
    return await _minioClient.GetObjectAsync(args).ConfigureAwait(false);
  }

  private bool _disposedValue;

  ~StorageMinioProvider() => Dispose(false);

  // Public implementation of Dispose pattern callable by consumers.
  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  // Protected implementation of Dispose pattern.
  protected virtual void Dispose(bool disposing)
  {
    if (!_disposedValue)
    {
      if (disposing)
      {
        _minioClient.Dispose();
      }
      _disposedValue = true;
    }
  }


}
