using SpotLights.Infrastructure.Interfaces.Identity;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Infrastructure.Manager.Storages;
using SpotLights.Infrastructure.Provider;
using SpotLights.Infrastructure.Repositories.Identity;
using SpotLights.Shared;
using SpotLights.Shared.Dtos;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Repositories.Posts;

public class ImportRepository :  IImportRepository
{
  private readonly IUserRepository _userProvider;
  private readonly ReverseProvider _reverseProvider;
  private readonly IPostRepository _postProvider;
  private readonly StorageManager _storageManager;

  public ImportRepository(
      IUserRepository userProvider,
      ReverseProvider reverseProvider,
      IPostRepository postProvider,
      StorageManager storageManager
  )
  {
    _userProvider = userProvider;
    _reverseProvider = reverseProvider;
    _postProvider = postProvider;
    _storageManager = storageManager;
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
        _ = await _storageManager.UploadAsync(
            publishedAt,
            user.Id,
            baseAddress,
            post.Cover
        );
      }

      string uploadeContent = await _storageManager.UploadsFoHtmlAsync(
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
