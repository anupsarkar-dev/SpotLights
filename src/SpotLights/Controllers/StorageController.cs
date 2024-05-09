using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.IO;
using System.Threading.Tasks;
using SpotLights.Shared.Constants;
using SpotLights.Infrastructure.Interfaces.Storages;

namespace SpotLights.Controllers;

public class StorageController : ControllerBase
{
    private readonly IStorageProvider _storageProvider;

    public StorageController(IStorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
    }

    [HttpGet($"{SpotLightsConstant.StorageRowPhysicalRoot}/{{**slug}}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
    [OutputCache(PolicyName = SpotLightsConstant.OutputCacheExpire1)]
    public async Task<IActionResult> GetAsync([FromRoute] string slug)
    {
        MemoryStream memoryStream = new();
        Shared.StorageDto? storage = await _storageProvider.GetAsync(
            slug,
            (stream, cancellationToken) => stream.CopyToAsync(memoryStream, cancellationToken)
        );
        if (storage == null)
            return NotFound();
        memoryStream.Position = 0;
        return File(memoryStream, storage.ContentType);
    }
}
