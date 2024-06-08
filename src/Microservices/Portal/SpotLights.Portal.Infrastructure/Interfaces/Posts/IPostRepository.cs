using SpotLights.Domain.Model.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Dtos;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Interfaces.Posts
{
    internal interface IPostRepository : IBaseContextRepository
    {
        Task<IEnumerable<PostEditorDto>> AddAsync(IEnumerable<PostEditorDto> posts, int userId);
        Task<string> AddAsync(PostEditorDto postInput, int userId);
        Task<PostDto> FirstAsync(DefaultIdType id);
        Task<IEnumerable<PostDto>> GetAsync();
        Task<PostDto> GetAsync(DefaultIdType id);
        Task<IEnumerable<PostItemDto>> GetAsync(
            PublishedStatus filter,
            PostType postType,
            int userId,
            bool isAdmin
        );
        Task<PostSlugDto> GetAsync(string slug);
        Task<PostPagerDto> GetByCategoryAsync(
            string category,
            int page,
            int pageSize,
            int userId,
            bool isAdmin
        );
        Task<PostEditorDto> GetEditorAsync(string slug);
        Task<PostPagerDto> GetPostsAsync(int page, int pageSize);
        Task<IEnumerable<PostItemDto>> GetSearchAsync(string term);
        Task<PostPagerDto> GetSearchAsync(string term, int page, int pageSize);
        Task<List<PostEditorDto>> MatchTitleAsync(IEnumerable<string> titles);
        Task StateAsync(IEnumerable<int> ids, PostState state);
        Task StateAsync(int id, PostState state);
        Task StateInternalAsync(IQueryable<Post> query, PostState state);
        Task UpdateAsync(PostEditorDto postInput, int userId, bool isAdmin);
    }
}
