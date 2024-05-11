using SpotLights.Shared;

namespace SpotLights.Core.Interfaces.Post
{
    internal interface IImportRssService
    {
        ImportDto Analysis(string feedUrl);
    }
}
