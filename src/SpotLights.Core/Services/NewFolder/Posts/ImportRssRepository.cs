using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Posts;

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
