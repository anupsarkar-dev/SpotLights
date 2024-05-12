using Mapster;
using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Infrastructure.Interfaces.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

internal class NewsletterRepository : BaseContextRepository, INewsletterRepository
{
    public NewsletterRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<IEnumerable<NewsletterDto>> GetItemsAsync(int userId, bool isAdmin)
    {
        IOrderedQueryable<Newsletter> query = _context.Newsletters
            .AsNoTracking()
            .Include(n => n.Post)
            .OrderByDescending(n => n.CreatedAt);

        return await query.ProjectToType<NewsletterDto>().ToListAsync();
    }

    public async Task<NewsletterDto?> FirstOrDefaultByPostIdAsync(int postId)
    {
        IQueryable<Newsletter> query = _context.Newsletters.Where(m => m.PostId == postId);

        return await query.ProjectToType<NewsletterDto>().FirstOrDefaultAsync();
    }

    public async Task AddNewsletterAsync(int postId, bool success)
    {
        Newsletter entry = new() { PostId = postId, Success = success, };
        await AddAsync(entry);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, bool success)
    {
        _ = await _context.Newsletters
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Success, success));

        await SaveChangesAsync();
    }
}
