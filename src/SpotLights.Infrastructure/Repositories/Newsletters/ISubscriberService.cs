using Mapster;
using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

public class ISubscriberService : BaseContextRepository, ISubscriberRepository
{
    public ISubscriberService(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
    {
        IOrderedQueryable<Subscriber> query = _context.Subscribers
            .AsNoTracking()
            .OrderByDescending(n => n.CreatedAt);

        return await query.ProjectToType<SubscriberDto>().ToListAsync();
    }

    public async Task<int> ApplyAsync(SubscriberApplyDto input)
    {
        if (await _context.Subscribers.AnyAsync(m => m.Email == input.Email))
        {
            return 0;
        }
        else
        {
            Subscriber data = input.Adapt<Subscriber>();
            _ = _context.Subscribers.Add(data);
            _ = await _context.SaveChangesAsync();

            return 1;
        }
    }
}
