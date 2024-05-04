using Mapster;
using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Model.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Data.Repositories.Newsletters;

public class NewsletterProvider : AppProvider<Newsletter, int>
{

  public NewsletterProvider(AppDbContext dbContext)
      : base(dbContext)
  {
  }

  public async Task<IEnumerable<NewsletterDto>> GetItemsAsync(int userId, bool isAdmin)
  {
    IOrderedQueryable<Newsletter> query = _dbContext.Newsletters
        .AsNoTracking()
        .Include(n => n.Post)
        .OrderByDescending(n => n.CreatedAt);

    return await query.ProjectToType<NewsletterDto>().ToListAsync();
  }

  public async Task<NewsletterDto?> FirstOrDefaultByPostIdAsync(int postId)
  {
    IQueryable<Newsletter> query = _dbContext.Newsletters.Where(m => m.PostId == postId);

    return await query.ProjectToType<NewsletterDto>().FirstOrDefaultAsync();
  }

  public async Task AddAsync(int postId, bool success)
  {
    Newsletter entry = new() { PostId = postId, Success = success, };
    await AddAsync(entry);
  }

  public async Task UpdateAsync(int id, bool success)
  {
    _ = await _dbContext.Newsletters
        .Where(m => m.Id == id)
        .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Success, success));
  }
}
