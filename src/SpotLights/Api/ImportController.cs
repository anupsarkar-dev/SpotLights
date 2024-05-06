using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Shared.Dtos;
using SpotLights.Infrastructure.Repositories.Posts;

namespace SpotLights.Interfaces;

[Route("api/import")]
[Authorize]
[ApiController]
public class ImportController : ControllerBase
{
    private readonly ImportRepository _importManager;

    public ImportController(ImportRepository importManager)
    {
        _importManager = importManager;
    }

    [HttpGet("rss")]
    public ImportDto Rss(
        [FromQuery] ImportRssDto request,
        [FromServices] ImportRssRepository importRssProvider
    )
    {
        return importRssProvider.Analysis(request.FeedUrl);
    }

    [HttpPost("write")]
    public async Task<IEnumerable<PostEditorDto>> Write([FromBody] ImportDto request)
    {
        int userId = User.FirstUserId();
        return await _importManager.WriteAsync(request, userId);
    }
}
