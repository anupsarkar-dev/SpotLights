using Markdig.Extensions.MediaLinks;
using SpotLights.Core.Interfaces.Post;
using SpotLights.Core.Interfaces.Provider;
using SpotLights.Infrastructure.Interfaces.Identity;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Infrastructure.Interfaces.Storages;
using SpotLights.Infrastructure.Provider;
using SpotLights.Shared;
using SpotLights.Shared.Dtos;
using SpotLights.Shared.Enums;

namespace SpotLights.Core.Services.Posts;

internal class ImportService : IImportService
{
 
  private readonly IUserRepository _userProvider;
  private readonly ReverseProvider _reverseProvider;
  private readonly IPostRepository _postProvider;
  private readonly IStorageProvider _storageProvider;

  public ImportService(
      IUserRepository userProvider,
      ReverseProvider reverseProvider,
      IPostRepository postProvider,
      IStorageProvider provider
  )
  {
    _userProvider = userProvider;
    _reverseProvider = reverseProvider;
    _postProvider = postProvider;
    _storageProvider = provider;
  }

  public async Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, int userId)
  {
    UserDto user = await _userProvider.FirstByIdAsync(userId);
    IEnumerable<string> titles = request.Posts.Select(m => m.Title);
    List<PostEditorDto> matchPosts = await _postProvider.MatchTitleAsync(titles);

    List<PostEditorDto> posts = [];

    foreach (PostEditorDto post in request.Posts)
    {
      PostEditorDto? postDb = matchPosts.FirstOrDefault(
          m => m.Title.Equals(post.Title, StringComparison.OrdinalIgnoreCase)
      );
      if (postDb != null)
      {
        posts.Add(postDb);
        continue;
      }

      DateTime publishedAt = post.PublishedAt!.Value.ToUniversalTime();
      Uri baseAddress = new(post.Slug!);
      if (!string.IsNullOrEmpty(post.Cover))
      {
        _ = await _storageProvider.UploadAsync(
            publishedAt,
            user.Id,
            baseAddress,
            post.Cover
        );
      }

      string uploadeContent = await _storageProvider.UploadsFoHtmlAsync(
      publishedAt,
      user.Id,
      baseAddress,
          post.Content
      );

      string markdownContent = _reverseProvider.ToMarkdown(uploadeContent);
      post.Content = markdownContent;

      string markdownDescription = _reverseProvider.ToMarkdown(post.Description);
      post.Description = markdownDescription;

      post.State = PostState.Release;
      post.PublishedAt = publishedAt;
      posts.Add(post);
    }

    return await _postProvider.AddAsync(posts, userId);
  }
}
