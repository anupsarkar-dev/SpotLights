using SpotLights.Infrastructure.Interfaces;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Posts;

public class PostManager : IPostManagerRepository
{
    private readonly PostRepository _postProvider;
    private readonly MarkdigRepository _markdigProvider;

    public PostManager(PostRepository postProvider, MarkdigRepository markdigProvider)
    {
        _postProvider = postProvider;
        _markdigProvider = markdigProvider;
    }

    public async Task<PostSlugDto> GetToHtmlAsync(string slug)
    {
        PostSlugDto postSlug = await _postProvider.GetAsync(slug);
        postSlug.Post.ContentHtml = _markdigProvider.ToHtml(postSlug.Post.Content);
        postSlug.Post.DescriptionHtml = _markdigProvider.ToHtml(postSlug.Post.Description);

        foreach (PostToHtmlDto related in postSlug.Related)
        {
            PostToHtmlDto relatedDto = postSlug.Related.First(m => m.Id == related.Id);
            relatedDto.DescriptionHtml = _markdigProvider.ToHtml(related.Description);
        }
        return postSlug;
    }
}
