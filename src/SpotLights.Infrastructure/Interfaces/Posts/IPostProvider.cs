using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces.Posts
{
    public interface IPostProvider
    {
        Task<PostSlugDto> GetToHtmlAsync(string slug);
    }
}
