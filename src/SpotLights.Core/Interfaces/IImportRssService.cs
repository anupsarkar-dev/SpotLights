using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    public interface IImportRssService
    {
        ImportDto Analysis(string feedUrl);
    }
}
