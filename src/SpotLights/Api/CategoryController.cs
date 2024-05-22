using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Domain.Model.Posts;
using SpotLights.Core.Interfaces;
using SpotLights.Shared.Extensions;

namespace SpotLights.Interfaces;

[Route("api/category")]
[Authorize]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryItemDto>> GetAll()
    {
        return await _categoryService.GetAllAsync();
    }

    [HttpGet("items")]
    public async Task<IEnumerable<CategoryItemDto>> GetItemsAsync()
    {
        return await _categoryService.GetItemsAsync();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        if (!User.IsAdmin())
        {
            return StatusCode(
                403,
                new { error = "User does not have permission to delete category." }
            );
        }

        await _categoryService.DeleteAsync<Category>(id);
        return Ok();
    }

    [HttpDelete("{idsString}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string idsString)
    {
        if (!User.IsAdmin())
        {
            return StatusCode(
                403,
                new { error = "User does not have permission to delete category." }
            );
        }

        IEnumerable<int> ids = idsString.Split(',').Select(int.Parse);
        await _categoryService.DeleteAsync<Category>(ids);

        return Ok();
    }

    [HttpGet("{postId:int}")]
    public async Task<IEnumerable<Category>> GetPostCategories(int postId)
    {
        return await _categoryService.GetPostCategories(postId);
    }

    [HttpGet("byId/{id:int}")]
    public async Task<Category> GetCategory(int id)
    {
        return await _categoryService.GetCategory(id);
    }

    [HttpGet("{term}")]
    public async Task<List<CategoryItemDto>> SearchCategories(string term = "*")
    {
        return await _categoryService.SearchCategories(term);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> SaveCategory(Category category)
    {
        return await _categoryService.SaveCategory(category);
    }

    [HttpPut("updateCategoryMenuStatus/{categoryId}")]
    public async Task<ActionResult<bool>> UpdateCategoryMenuStatus(
        int categoryId,
        [FromBody] bool status
    )
    {
        if (categoryId <= 0)
            return BadRequest("Invalid input request.");

        return await _categoryService.UpdateCategoryMenusStatusByIdAsync(categoryId, status);
    }
}
