using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    public interface IPostManagerService
    {
        Task<PostSlugDto> GetToHtmlAsync(string slug);
    }
}
