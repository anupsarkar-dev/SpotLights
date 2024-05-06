using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface IPostManagerRepository
    {
        Task<PostSlugDto> GetToHtmlAsync(string slug);
    }
}
