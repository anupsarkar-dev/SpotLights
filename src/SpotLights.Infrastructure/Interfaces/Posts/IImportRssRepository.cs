using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces.Posts
{
    internal interface IImportRssRepository
    {
        ImportDto Analysis(string feedUrl);
    }
}
