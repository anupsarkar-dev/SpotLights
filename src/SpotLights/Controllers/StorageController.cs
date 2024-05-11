using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.IO;
using System.Threading.Tasks;
using SpotLights.Shared.Constants;
using SpotLights.Infrastructure.Interfaces.Storages;
using SpotLights.Core.Interfaces.Provider;

namespace SpotLights.Controllers;

internal class StorageController : ControllerBase
{
    private readonly IStorageManager _manager;

    public StorageController(IStorageManager manager)
    {
        _manager = manager;
    }

    [HttpGet($"{SpotLightsConstant.StorageRowPhysicalRoot}/{{**slug}}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
    [OutputCache(PolicyName = SpotLightsConstant.OutputCacheExpire1)]
    public async Task<IActionResult> GetAsync([FromRoute] string slug)
    {
        MemoryStream memoryStream = new();
        Shared.StorageDto? storage = await _manager.GetAsync(
            slug,
            (stream, cancellationToken) => stream.CopyToAsync(memoryStream, cancellationToken)
        );
        if (storage == null)
            return NotFound();
        memoryStream.Position = 0;
        return File(memoryStream, storage.ContentType);
    }
}
