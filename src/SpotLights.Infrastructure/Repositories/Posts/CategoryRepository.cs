using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Posts;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Posts;

internal class CategoryRepository : BaseContextRepository, ICategoryRepository
{
  public CategoryRepository(ApplicationDbContext dbContext)
      : base(dbContext) { }

  public async Task<List<CategoryItemDto>> GetItemsAsync()
  {
    return !_context.Categories.Any()
        ? new()
        : await _context.Categories
            .GroupBy(
                m =>
                    new
                    {
                      m.Id,
                      m.Content,
                      m.Description,
                      m.ShowInMenu
                    }
            )
            .Select(
                m =>
                    new CategoryItemDto
                    {
                      Id = m.Key.Id,
                      Category = m.Key.Content,
                      Description = m.Key.Description,
                      ShowInMenu = m.Key.ShowInMenu,
                      PostCount = _context.PostCategories.Count(s => s.CategoryId == m.Key.Id)
                    }
            )
            .AsNoTracking()
            .ToListAsync();
  }

  public async Task<List<CategoryItemDto>> GetAllAsync()
  {
    return await _context.Categories.Select(c => new CategoryItemDto()
    {
      Id = c.Id,
      Category = c.Content,
      Description = c.Description,
      ShowInMenu = c.ShowInMenu,
    }).AsNoTracking().ToListAsync();
  }

  public async Task<List<CategoryItemDto>> GetItemsExistPostAsync()
  {
    return await _context.PostCategories
        .Include(pc => pc.Category)
        .GroupBy(
            m =>
                new
                {
                  m.Category.Id,
                  m.Category.Content,
                  m.Category.Description,
                  m.Category.ShowInMenu
                }
        )
        .Select(
            m =>
                new CategoryItemDto
                {
                  Id = m.Key.Id,
                  Category = m.Key.Content,
                  Description = m.Key.Description,
                  ShowInMenu = m.Key.ShowInMenu,
                  PostCount = m.Count()
                }
        )
        .AsNoTracking()
        .ToListAsync();
  }

  public async Task<List<CategoryItemDto>> SearchCategories(string term)
  {
    List<CategoryItemDto> cats = await GetItemsAsync();

    return term == "*"
        ? cats
        : cats.Where(c => c.Category.ToLower().Contains(term.ToLower())).ToList();
  }

  public async Task<bool> UpdateCategoryMenusStatusByIdAsync(int categoryId, bool status)
  {
    if (categoryId <= 0) return false;

    var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
    if (existingCategory is null) return false;

    existingCategory.ShowInMenu = status;
    return await _context.SaveChangesAsync() > 0;
  }

  public async Task<Category> GetCategory(int categoryId)
  {
    return await _context.Categories.AsNoTracking().Where(c => c.Id == categoryId).FirstAsync();
  }

  public async Task<IEnumerable<Category>> GetPostCategories(int postId)
  {
    return await _context.Posts
        .AsNoTracking()
        .Where(pc => pc.Id == postId)
        .SelectMany(pc => pc.PostCategories!, (parent, child) => child.Category)
        .ToListAsync();
  }

  public async Task<bool> SaveCategory(Category category)
  {
    Category? dbCategory = await _context.Categories
        .Where(c => c.Id == category.Id)
        .FirstOrDefaultAsync();

    if (dbCategory == null)
    {
      return false;
    }

    dbCategory.Content = category.Content;
    dbCategory.Description = category.Description;
    dbCategory.ShowInMenu = category.ShowInMenu;
    return await _context.SaveChangesAsync() > 0;
  }

  public async Task<Category> SaveCategory(string tag)
  {
    Category? category = await _context.Categories
        .AsNoTracking()
        .Where(c => c.Content == tag)
        .FirstOrDefaultAsync();

    if (category != null)
    {
      return category;
    }

    category = new Category() { Content = tag, CreatedAt = DateTime.UtcNow };
    _ = _context.Categories.Add(category);
    _ = await _context.SaveChangesAsync();

    return category;
  }
}
