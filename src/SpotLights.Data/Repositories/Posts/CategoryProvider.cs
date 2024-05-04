using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Model.Posts;
using SpotLights.Shared;

namespace SpotLights.Data.Repositories.Posts;

public class CategoryProvider : AppProvider<Category, int>
{
  public CategoryProvider(AppDbContext dbContext) : base(dbContext)
  {
  }

  public async Task<List<CategoryItemDto>> GetItemsAsync()
  {
    return !_dbContext.Categories.Any()
      ? (List<CategoryItemDto>)([])
      : await _dbContext.Categories
        .GroupBy(m => new { m.Id, m.Content, m.Description })
        .Select(m => new CategoryItemDto
        {
          Id = m.Key.Id,
          Category = m.Key.Content,
          Description = m.Key.Description,
          PostCount = _dbContext.PostCategories.Count(s => s.CategoryId == m.Key.Id)
        })
        .AsNoTracking()
        .ToListAsync();
  }

  public async Task<List<CategoryItemDto>> GetItemsExistPostAsync()
  {
    return await _dbContext.PostCategories
      .Include(pc => pc.Category)
      .GroupBy(m => new { m.Category.Id, m.Category.Content, m.Category.Description })
      .Select(m => new CategoryItemDto
      {
        Id = m.Key.Id,
        Category = m.Key.Content,
        Description = m.Key.Description,
        PostCount = m.Count()
      })
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<List<CategoryItemDto>> SearchCategories(string term)
  {
    List<CategoryItemDto> cats = await GetItemsAsync();

    return term == "*" ? cats : cats.Where(c => c.Category.ToLower().Contains(term.ToLower())).ToList();
  }

  public async Task<Category> GetCategory(int categoryId)
  {
    return await _dbContext.Categories
      .AsNoTracking()
      .Where(c => c.Id == categoryId)
      .FirstAsync();
  }

  public async Task<IEnumerable<Category>> GetPostCategories(int postId)
  {
    return await _dbContext.Posts
        .AsNoTracking()
        .Where(pc => pc.Id == postId)
        .SelectMany(pc => pc.PostCategories!, (parent, child) => child.Category)
        .ToListAsync();
  }

  public async Task<bool> SaveCategory(Category category)
  {
    Category? dbCategory = await _dbContext.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync();

    if (dbCategory == null)
    {
      return false;
    }

    dbCategory.Content = category.Content;
    dbCategory.Description = category.Description;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<Category> SaveCategory(string tag)
  {
    Category? category = await _dbContext.Categories
        .AsNoTracking()
        .Where(c => c.Content == tag)
        .FirstOrDefaultAsync();

    if (category != null)
    {
      return category;
    }

    category = new Category()
    {
      Content = tag,
      CreatedAt = DateTime.UtcNow
    };
    _ = _dbContext.Categories.Add(category);
    _ = await _dbContext.SaveChangesAsync();

    return category;
  }
}
