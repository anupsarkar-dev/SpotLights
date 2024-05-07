using Microsoft.EntityFrameworkCore;
using SpotLights.Helper;
using SpotLights.Shared;
using SpotLights.Shared.Extensions;
using System.Text.RegularExpressions;
using Mapster;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Posts;
using SpotLights.Shared.Enums;
using SpotLights.Shared.Dtos;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Domain.Dto;

namespace SpotLights.Infrastructure.Repositories.Posts;

public class IPostService : BaseContextRepository, IPostRepository
{

  public IPostService(ApplicationDbContext dbContext)
      : base(dbContext)
  {
  }

  public async Task<PostDto> FirstAsync(int id)
  {
    IQueryable<Post> query = _context.Posts.Where(p => p.Id == id);
    return await query.ProjectToType<PostDto>().FirstAsync();
  }

  public async Task<PostDto> GetAsync(int id)
  {
    var query = _context.Posts.AsNoTracking().Where(p => p.Id == id);
    return await query.ProjectToType<PostDto>().FirstAsync();
  }

  public async Task<IEnumerable<PostDto>> GetAsync()
  {
    IOrderedQueryable<Post> query = _context.Posts
        .AsNoTracking()
        .Include(pc => pc.User)
        .OrderByDescending(m => m.CreatedAt);
    return await query.ProjectToType<PostDto>().ToListAsync();
  }

  public async Task<PostSlugDto> GetAsync(string slug)
  {
    IQueryable<Post> postQuery = _context.Posts.Include(m => m.User).Where(p => p.Slug == slug);

    var post = await postQuery.ProjectToType<PostToHtmlDto>().FirstAsync();

    post.Views++;

    _ = await _context.SaveChangesAsync();

    IOrderedQueryable<Post> olderQuery = _context.Posts
        .AsNoTracking()
        .Where(m => m.State >= PostState.Release && m.PublishedAt > post.PublishedAt)
        .OrderBy(p => p.PublishedAt);

    var older = await olderQuery.ProjectToType<PostItemDto>().FirstOrDefaultAsync();

    IOrderedQueryable<Post> newerQuery = _context.Posts
        .AsNoTracking()
        .Where(m => m.State >= PostState.Release && m.PublishedAt < post.PublishedAt)
        .OrderByDescending(p => p.PublishedAt);

    var newer = await newerQuery.ProjectToType<PostItemDto>().FirstOrDefaultAsync();

    IQueryable<Post> relatedQuery = _context.Posts
        .AsNoTracking()
        .Include(m => m.User)
        .Where(m => m.State == PostState.Featured && m.Id != post.Id);

    if (older != null)
    {
      relatedQuery = relatedQuery.Where(m => m.Id != older.Id);
    }

    if (newer != null)
    {
      relatedQuery = relatedQuery.Where(m => m.Id != newer.Id);
    }

    relatedQuery = relatedQuery.OrderByDescending(p => p.PublishedAt).Take(3);
    var related = await relatedQuery.ProjectToType<PostToHtmlDto>().ToListAsync();

    return new PostSlugDto
    {
      Post = post,
      Older = older,
      Newer = newer,
      Related = related
    };
  }

  public async Task<PostPagerDto> GetPostsAsync(int page, int pageSize)
  {
    int skip = (page - 1) * pageSize;
    IQueryable<Post> query = _context.Posts
        .AsNoTracking()
        .Include(pc => pc.User)
        .Where(m => m.PostType == PostType.Post && m.State >= PostState.Release);

    int total = await query.CountAsync();
    query = query.OrderByDescending(m => m.CreatedAt).Skip(skip).Take(pageSize);
    var items = await query.ProjectToType<PostItemDto>().ToListAsync();
    return new PostPagerDto(items, total, page, pageSize);
  }

  public async Task<PostEditorDto> GetEditorAsync(string slug)
  {
    IQueryable<Post> query = _context.Posts
        .AsNoTracking()
        .Include(m => m.PostCategories)!
        .ThenInclude(m => m.Category)
        //.Include(m => m.StorageReferences!.Where(s => s.Type == StorageReferenceType.Post))
        .AsSingleQuery()
        .Where(p => p.Slug == slug);
    return await query.ProjectToType<PostEditorDto>().FirstAsync();
  }

  public async Task<List<PostEditorDto>> MatchTitleAsync(IEnumerable<string> titles)
  {
    IQueryable<Post> query = _context.Posts
        .AsNoTracking()
        .Include(m => m.PostCategories)!
        .ThenInclude(m => m.Category)
        .Where(p => titles.Contains(p.Title));
    return await query.ProjectToType<PostEditorDto>().ToListAsync();
  }

  public async Task<IEnumerable<PostItemDto>> GetAsync(
      PublishedStatus filter,
      PostType postType,
      int userId,
      bool isAdmin
  )
  {
    IQueryable<Post> query = _context.Posts.AsNoTracking().Where(p => p.PostType == postType);

    if (!isAdmin)
    {
      query = query.Where(s => s.UserId == userId);
    }

    query = filter switch
    {
      PublishedStatus.Featured | PublishedStatus.Published
          => query
              .Where(p => p.State >= PostState.Release)
              .OrderByDescending(p => p.PublishedAt),
      PublishedStatus.Drafts
          => query.Where(p => p.State == PostState.Draft).OrderByDescending(p => p.Id),
      _ => query.OrderByDescending(p => p.PublishedAt).ThenByDescending(p => p.CreatedAt),
    };

    return await query.ProjectToType<PostItemDto>().ToListAsync();
  }

  public async Task<PostPagerDto> GetSearchAsync(string term, int page, int pageSize)
  {
    IQueryable<PostItemDto> query = _context.Posts
        .Include(pc => pc.User)
        .Include(pc => pc.PostCategories)
        .ThenInclude(pc => pc.Category)
        .Where(
            m =>
                m.Title.Contains(term)
                || m.Description.Contains(term)
                || m.Content.Contains(term)
        )
        .AsNoTracking()
        .Select(
            s =>
                new PostItemDto
                {
                  Id = s.Id,
                  User = new UserDto
                  {
                    Id = s.User.Id,
                    Email = s.User.Email ?? "",
                    NickName = s.User.NickName,
                    Avatar = s.User.Avatar,
                    Bio = s.User.Bio
                  },
                  Title = s.Title,
                  Slug = s.Slug,
                  Description = s.Description,
                  Cover = s.Cover,
                  State = s.State,
                  PublishedAt = s.PublishedAt,
                  Categories =
                        s.PostCategories != null
                            ? s.PostCategories
                                .Select(
                                    c =>
                                        new CategoryDto
                                        {
                                          Id = c.CategoryId,
                                          Content = c.Category.Content
                                        }
                                )
                                .ToList()
                            : null
                }
        );

    List<PostItemDto> posts = await query.ToListAsync();

    List<PostSearchDto> postsSearch = new();
    List<string> termList = term.ToLower().Split(' ').ToList();

    foreach (PostItemDto? post in posts)
    {
      int rank = 0;
      int hits = 0;

      foreach (string? termItem in termList)
      {
        if (termItem.Length < 4 && rank > 0)
        {
          continue;
        }

        if (post.Categories != null)
        {
          foreach (CategoryDto category in post.Categories)
          {
            if (category.Content.ToLower() == termItem)
            {
              rank += 10;
            }
          }
        }
        if (post.Title.ToLower().Contains(termItem))
        {
          hits = Regex.Matches(post.Title.ToLower(), termItem).Count;
          rank += hits * 10;
        }
        if (post.Description.ToLower().Contains(termItem))
        {
          hits = Regex.Matches(post.Description.ToLower(), termItem).Count;
          rank += hits * 3;
        }
        //if (post.Content.ToLower().Contains(termItem))
        //{
        //  rank += Regex.Matches(post.Content.ToLower(), termItem).Count;
        //}
      }

      if (rank > 0)
      {
        postsSearch.Add(new PostSearchDto(post, rank));
      }
    }

    int total = postsSearch.Count;
    int skip = (page * pageSize) - pageSize;
    List<PostItemDto> items = postsSearch
        .OrderByDescending(r => r.Rank)
        .Skip(skip)
        .Take(pageSize)
        .Select(m => m.Post)
        .ToList();

    return new PostPagerDto(items, total, page, pageSize);
  }

  public async Task<PostPagerDto> GetByCategoryAsync(
      string category,
      int page,
      int pageSize,
      int userId,
      bool isAdmin
  )
  {
    int skip = (page - 1) * pageSize;
    IQueryable<Post> query = _context.Categories
        .AsNoTracking()
        .Include(pc => pc.PostCategories)!
        .ThenInclude(m => m.Post)
        .ThenInclude(m => m.User)
        .Where(m => m.Content.Contains(category))
        .SelectMany(pc => pc.PostCategories!, (parent, child) => child.Post);

    if (!isAdmin)
    {
      query = query.Where(s => s.UserId == userId);
    }

    int total = await query.CountAsync();
    query = query.Skip(skip).Take(pageSize);
    var items = await query.ProjectToType<PostItemDto>().ToListAsync();
    return new PostPagerDto(items, total, page, pageSize);
  }

  public async Task<IEnumerable<PostItemDto>> GetSearchAsync(string term)
  {
    IQueryable<Post> query = _context.Posts.AsNoTracking();
    if ("*".Equals(term, StringComparison.Ordinal))
    {
      query = query.Where(p => p.Title.ToLower().Contains(term.ToLower()));
    }

    return await query.ProjectToType<PostItemDto>().ToListAsync();
  }

  public Task StateAsync(int id, PostState state)
  {
    IQueryable<Post> query = _context.Posts.Where(p => p.Id == id);
    return StateInternalAsync(query, state);
  }

  public Task StateAsync(IEnumerable<int> ids, PostState state)
  {
    IQueryable<Post> query = _context.Posts.Where(p => ids.Contains(p.Id));
    return StateInternalAsync(query, state);
  }

  public async Task StateInternalAsync(IQueryable<Post> query, PostState state)
  {
    _ = await query.ExecuteUpdateAsync(setters => setters.SetProperty(b => b.State, state));
  }

  public async Task<string> AddAsync(PostEditorDto postInput, int userId)
  {
    Post post = await AddInternalAsync(postInput, userId);
    _ = await _context.SaveChangesAsync();
    return post.Slug;
  }

  private async Task<Post> AddInternalAsync(PostEditorDto postInput, int userId)
  {
    string slug = await GetSlugFromTitle(postInput.Title);
    List<PostCategory>? postCategories = await CheckPostCategories(postInput.Categories);

    string contentScriptFiltr = StringHelper
        .HtmlScriptGeneratedRegex()
        .Replace(postInput.Content, string.Empty);
    string descriptionScriptFiltr = StringHelper
        .HtmlScriptGeneratedRegex()
        .Replace(postInput.Description, string.Empty);
    string contentFiltr = StringHelper
        .HtmlImgGeneratedRegex()
        .Replace(contentScriptFiltr, string.Empty);
    string descriptionFiltr = StringHelper
        .HtmlImgGeneratedRegex()
        .Replace(descriptionScriptFiltr, string.Empty);

    DateTime? publishedAt = GetPublishedAt(postInput.PublishedAt, postInput.State);
    Post post = new()
    {
      UserId = userId,
      Title = postInput.Title,
      Slug = slug,
      Content = contentFiltr,
      Description = descriptionFiltr,
      Cover = postInput.Cover,
      PostType = postInput.PostType,
      State = postInput.State,
      PublishedAt = publishedAt,
      PostCategories = postCategories,
    };
    _ = _context.Posts.Add(post);
    return post;
  }

  private static DateTime? GetPublishedAt(DateTime? inputPublishedAt, PostState inputState)
  {
    if (inputState >= PostState.Release)
    {
      return !inputPublishedAt.HasValue ? DateTime.UtcNow : inputPublishedAt;
    }
    else
    {
      return null;
    }
  }

  public async Task<IEnumerable<PostEditorDto>> AddAsync(
      IEnumerable<PostEditorDto> posts,
      int userId
  )
  {
    List<Post> postsInput = new();
    foreach (PostEditorDto post in posts)
    {
      Post postInput = await AddInternalAsync(post, userId);
      postsInput.Add(postInput);
    }
    _ = await _context.SaveChangesAsync();
    return postsInput.Adapt<IEnumerable<PostEditorDto>>();
  }

  public async Task UpdateAsync(PostEditorDto postInput, int userId)
  {
    Post post = await _context.Posts
        .Include(m => m.PostCategories)!
        .ThenInclude(m => m.Category)
        .FirstAsync(m => m.Id == postInput.Id);

    if (post.UserId != userId)
    {
      throw new BlogNotIitializeException();
    }

    List<PostCategory>? postCategories = await CheckPostCategories(postInput.Categories, post.PostCategories);

    post.Slug = postInput.Slug!;
    post.Title = postInput.Title;

    string contentScriptFiltr = StringHelper
        .HtmlScriptGeneratedRegex()
        .Replace(postInput.Content, string.Empty);
    string descriptionScriptFiltr = StringHelper
        .HtmlScriptGeneratedRegex()
        .Replace(postInput.Description, string.Empty);
    string contentFiltr = StringHelper
        .HtmlImgGeneratedRegex()
        .Replace(contentScriptFiltr, string.Empty);
    string descriptionFiltr = StringHelper
        .HtmlImgGeneratedRegex()
        .Replace(descriptionScriptFiltr, string.Empty);

    post.Description = descriptionFiltr;
    post.Content = contentFiltr;
    post.Cover = postInput.Cover;
    post.PostCategories = postCategories;
    post.PublishedAt = GetPublishedAt(postInput.PublishedAt, postInput.State);
    post.State = postInput.State;
    _ = _context.Update(post);
    _ = await _context.SaveChangesAsync();
  }

  private async Task<string> GetSlugFromTitle(string title)
  {
    string slug = title.ToSlug();
    int i = 1;
    string slugOriginal = slug;
    while (true)
    {
      if (!await _context.Posts.Where(p => p.Slug == slug).AnyAsync())
      {
        return slug;
      }

      i++;
      if (i >= 100)
      {
        throw new BlogNotIitializeException();
      }

      slug = $"{slugOriginal}{i}";
    }
  }

  private async Task<List<PostCategory>?> CheckPostCategories(
      List<CategoryDto>? input,
      List<PostCategory>? original = null
  )
  {
    if (input == null || !input.Any())
    {
      return null;
    }

    // 去重
    List<string> categories = input.GroupBy(d => new { d.Content }).Select(m => m.Key.Content).ToList();

    original = original == null
      ? (List<PostCategory>)([])
      : original
          .Where(p =>
          {
            string? item = categories.FirstOrDefault(
                      m => p.Category.Content.Equals(m, StringComparison.Ordinal)
                  );
            if (item != null)
            {
              _ = categories.Remove(item);
              return true;
            }
            return false;
          })
          .ToList();

    if (categories.Any())
    {
      List<Category> categoriesDb = await _context.Categories
          .Where(m => categories.Contains(m.Content))
          .ToListAsync();

      foreach (string? item in categories)
      {
        Category? categoryDb = categoriesDb.FirstOrDefault(
            m => item.Equals(m.Content, StringComparison.Ordinal)
        );
        original.Add(
            new PostCategory { Category = categoryDb ?? new Category { Content = item } }
        );
      }
    }
    return original;
  }
}
