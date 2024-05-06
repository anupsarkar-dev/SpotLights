using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces.Posts
{
    public interface IImportRssRepository
    {
        ImportDto Analysis(string feedUrl);
    }
}
