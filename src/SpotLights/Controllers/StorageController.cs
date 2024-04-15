using SpotLights.Storages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.IO;
using System.Threading.Tasks;

namespace SpotLights.Controllers;

public class StorageController : ControllerBase
{
  private readonly IStorageProvider _storageProvider;

  public StorageController(
    IStorageProvider storageProvider)
  {
    _storageProvider = storageProvider;
  }

  [HttpGet($"{SpotLightsConstant.StorageRowPhysicalRoot}/{{**slug}}")]
  [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
  [OutputCache(PolicyName = SpotLightsConstant.OutputCacheExpire1)]
  public async Task<IActionResult> GetAsync([FromRoute] string slug)
  {
    var memoryStream = new MemoryStream();
    var storage = await _storageProvider.GetAsync(slug,
      (stream, cancellationToken) => stream.CopyToAsync(memoryStream, cancellationToken));
    if (storage == null) return NotFound();
    memoryStream.Position = 0;
    return File(memoryStream, storage.ContentType);
  }
}
