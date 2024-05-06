using SpotLights.Shared;
using SpotLights.Domain.Model.Posts;
using SpotLights.Shared.Enums;
using SpotLights.Shared.Dtos;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Core.Interfaces;

namespace SpotLights.Infrastructure.Repositories.Posts;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostEditorDto>> AddAsync(
        IEnumerable<PostEditorDto> posts,
        int userId
    )
    {
        return await _postRepository.AddAsync(posts, userId);
    }

    public async Task<string> AddAsync(PostEditorDto postInput, int userId)
    {
        return await _postRepository.AddAsync(postInput, userId);
    }

    public async Task<PostDto> FirstAsync(int id)
    {
        return await _postRepository.FirstAsync(id);
    }

    public async Task<IEnumerable<PostDto>> GetAsync()
    {
        return await _postRepository.GetAsync();
    }

    public async Task<PostDto> GetAsync(int id)
    {
        return await _postRepository.GetAsync(id);
    }

    public async Task<IEnumerable<PostItemDto>> GetAsync(
        PublishedStatus filter,
        PostType postType,
        int userId,
        bool isAdmin
    )
    {
        return await _postRepository.GetAsync(filter, postType, userId, isAdmin);
    }

    public async Task<PostSlugDto> GetAsync(string slug)
    {
        return await _postRepository.GetAsync(slug);
    }

    public async Task<PostPagerDto> GetByCategoryAsync(
        string category,
        int page,
        int pageSize,
        int userId,
        bool isAdmin
    )
    {
        return await _postRepository.GetByCategoryAsync(category, page, pageSize, userId, isAdmin);
    }

    public async Task<PostEditorDto> GetEditorAsync(string slug)
    {
        return await _postRepository.GetEditorAsync(slug);
    }

    public async Task<PostPagerDto> GetPostsAsync(int page, int pageSize)
    {
        return await _postRepository.GetPostsAsync(page, pageSize);
    }

    public async Task<IEnumerable<PostItemDto>> GetSearchAsync(string term)
    {
        return await _postRepository.GetSearchAsync(term);
    }

    public async Task<PostPagerDto> GetSearchAsync(string term, int page, int pageSize)
    {
        return await _postRepository.GetSearchAsync(term, page, pageSize);
    }

    public async Task<List<PostEditorDto>> MatchTitleAsync(IEnumerable<string> titles)
    {
        return await _postRepository.MatchTitleAsync(titles);
    }

    public async Task StateAsync(IEnumerable<int> ids, PostState state)
    {
        await _postRepository.StateAsync(ids, state);
    }

    public async Task StateAsync(int id, PostState state)
    {
        await _postRepository.StateAsync(id, state);
    }

    public async Task StateInternalAsynct(IQueryable<Post> query, PostState state)
    {
        await _postRepository.StateInternalAsync(query, state);
    }

    public async Task UpdateAsync(PostEditorDto postInput, int userId)
    {
        await _postRepository.UpdateAsync(postInput, userId);
    }
}
