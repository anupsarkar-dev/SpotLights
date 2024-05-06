using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces.Posts
{
    public interface IPostManagerRepository
    {
        Task<PostSlugDto> GetToHtmlAsync(string slug);
    }
}
