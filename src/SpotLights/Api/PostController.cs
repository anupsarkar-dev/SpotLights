using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Shared.Dtos;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Shared.Enums;
using SpotLights.Infrastructure.Manager.Storages;
using SpotLights.Domain.Model.Posts;

namespace SpotLights.Interfaces;

[ApiController]
[Authorize]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly PostRepository _postProvider;

    public PostController(PostRepository postProvider)
    {
        _postProvider = postProvider;
    }

    [HttpGet("items/{filter}/{postType}")]
    public async Task<IEnumerable<PostItemDto>> GetItemsAsync(
        [FromRoute] PublishedStatus filter,
        [FromRoute] PostType postType
    )
    {
        int userId = User.FirstUserId();
        bool isAdmin = User.IsAdmin();
        return await _postProvider.GetAsync(filter, postType, userId, isAdmin);
    }

    [HttpGet("items/search/{term}")]
    public async Task<IEnumerable<PostItemDto>> GetSearchAsync([FromRoute] string term)
    {
        return await _postProvider.GetSearchAsync(term);
    }

    [HttpGet("byslug/{slug}")]
    public async Task<PostEditorDto> GetPostBySlug(string slug)
    {
        return await _postProvider.GetEditorAsync(slug);
    }

    [HttpPost("add")]
    [RequestSizeLimit(128 * 1024 * 1024)]
    public async Task<string> AddPostAsync(
        [FromServices] StorageManager storageManager,
        [FromBody] PostEditorDto post
    )
    {
        int userId = User.FirstUserId();
        DateTime uploadAt = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(post.Cover))
        {
            var coverUrl = await storageManager.UploadImagesBase64(uploadAt, userId, post.Cover);
            post.Cover = coverUrl;
        }
        var uploadContent = await storageManager.UploadImagesBase64FoHtml(
            uploadAt,
            userId,
            post.Content
        );
        post.Content = uploadContent;
        return await _postProvider.AddAsync(post, userId);
    }

    [HttpPut("update")]
    [RequestSizeLimit(128 * 1024 * 1024)]
    public async Task UpdateAsync(
        [FromServices] StorageManager storageManager,
        [FromBody] PostEditorDto post
    )
    {
        int userId = User.FirstUserId();
        DateTime uploadAt = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(post.Cover))
        {
            var coverUrl = await storageManager.UploadImagesBase64(uploadAt, userId, post.Cover);
            post.Cover = coverUrl;
        }
        var uploadContent = await storageManager.UploadImagesBase64FoHtml(
            uploadAt,
            userId,
            post.Content
        );
        post.Content = uploadContent;
        await _postProvider.UpdateAsync(post, userId);
    }

    [HttpPut("state/{id:int}")]
    public async Task StateAsynct([FromRoute] int id, [FromBody] PostState state)
    {
        await _postProvider.StateAsync(id, state);
    }

    [HttpPut("state/{idsString}")]
    public async Task StateAsynct([FromRoute] string idsString, [FromBody] PostState state)
    {
        IEnumerable<int> ids = idsString.Split(',').Select(int.Parse);
        await _postProvider.StateAsync(ids, state);
    }

    [HttpDelete("{id:int}")]
    public async Task DeleteAsync([FromRoute] int id)
    {
        await _postProvider.DeleteAsync<Post>(id);
        await _postProvider.SaveChangesAsync();
    }

    [HttpDelete("{idsString}")]
    public async Task DeleteAsync([FromRoute] string idsString)
    {
        IEnumerable<int> ids = idsString.Split(',').Select(int.Parse);
        await _postProvider.DeleteAsync<Post>(ids);
        await _postProvider.SaveChangesAsync();
    }
}
