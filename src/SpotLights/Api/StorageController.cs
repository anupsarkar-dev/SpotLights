using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Infrastructure.Manager.Storages;
using SpotLights.Infrastructure.Interfaces.Storages;
using SpotLights.Core.Interfaces.Provider;

namespace SpotLights.Interfaces;

[Route("api/storage")]
[ApiController]
[Authorize]
internal class StorageController : ControllerBase
{
    private readonly IStorageProvider _storageProvider;
    private readonly IStorageManager _storageManager;

    public StorageController(IStorageProvider storageProvider, IStorageManager storageManager)
    {
        _storageProvider = storageProvider;
        _storageManager = storageManager;
    }

    [HttpPut("exists")]
    public async Task<ActionResult> ExistsAsync([FromBody] string slug)
    {
        if (await _storageManager.ExistsAsync(slug))
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("upload")]
    public async Task<StorageDto?> Upload([FromForm] IFormFile file)
    {
        int userId = User.FirstUserId();
        DateTime currTime = DateTime.UtcNow;
        return await _storageProvider.UploadAsync(currTime, userId, file);
    }
}
