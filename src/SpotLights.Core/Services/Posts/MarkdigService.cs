using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces.Posts;

namespace SpotLights.Core.Services.Posts;

internal class MarkdigService : IMarkdigService
{
    private readonly IMarkdigRepository _markdigRepository;

    public MarkdigService(IMarkdigRepository markdigRepository)
    {
        _markdigRepository = markdigRepository;
    }

    public string ToHtml(string markdown)
    {
        return _markdigRepository.ToHtml(markdown);
    }
}
