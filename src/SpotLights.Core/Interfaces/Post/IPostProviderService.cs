using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    public interface IPostProviderService
    {
        Task<PostSlugDto> GetToHtmlAsync(string slug);
    }
}
