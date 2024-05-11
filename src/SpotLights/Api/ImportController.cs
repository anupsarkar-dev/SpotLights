using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Shared.Dtos;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Core.Interfaces.Post;

namespace SpotLights.Interfaces;

[Route("api/import")]
[Authorize]
[ApiController]
internal class ImportController : ControllerBase
{
    private readonly IImportService _importManager;

    public ImportController(IImportService importManager)
    {
        _importManager = importManager;
    }

    [HttpGet("rss")]
    public ImportDto Rss(
        [FromQuery] ImportRssDto request,
        [FromServices] IImportRssService importRssService
    )
    {
        return importRssService.Analysis(request.FeedUrl);
    }

    [HttpPost("write")]
    public async Task<IEnumerable<PostEditorDto>> Write([FromBody] ImportDto request)
    {
        int userId = User.FirstUserId();
        return await _importManager.WriteAsync(request, userId);
    }
}
