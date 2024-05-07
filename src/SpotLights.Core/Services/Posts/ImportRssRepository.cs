using SpotLights.Core.Interfaces.Post;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Shared;

namespace SpotLights.Core.Services.Posts;

public class ImportRssService : IImportRssService
{
    private readonly IImportRssRepository _importRssRepository;

    public ImportRssService(IImportRssRepository importRssRepository)
    {
        _importRssRepository = importRssRepository;
    }

    public ImportDto Analysis(string feedUrl)
    {
        return _importRssRepository.Analysis(feedUrl);
    }
}
