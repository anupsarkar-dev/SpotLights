using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    internal interface IPostProviderService
    {
        Task<PostSlugDto> GetToHtmlAsync(string slug);
    }
}
