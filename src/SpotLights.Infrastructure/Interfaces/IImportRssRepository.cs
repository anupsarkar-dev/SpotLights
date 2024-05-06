using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface IImportRssRepository
    {
        ImportDto Analysis(string feedUrl);
    }
}
