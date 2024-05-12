using SpotLights.Shared;

namespace SpotLights.Core.Interfaces.Post
{
    public interface IImportRssService
    {
        ImportDto Analysis(string feedUrl);
    }
}
