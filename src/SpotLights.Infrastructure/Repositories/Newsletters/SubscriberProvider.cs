using Mapster;
using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

public class SubscriberProvider : AppProvider<Subscriber, int>
{
    public SubscriberProvider(AppDbContext dbContext)
        : base(dbContext) { }

    public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
    {
        IOrderedQueryable<Subscriber> query = _dbContext.Subscribers
            .AsNoTracking()
            .OrderByDescending(n => n.CreatedAt);

        return await query.ProjectToType<SubscriberDto>().ToListAsync();
    }

    public async Task<int> ApplyAsync(SubscriberApplyDto input)
    {
        if (await _dbContext.Subscribers.AnyAsync(m => m.Email == input.Email))
        {
            return 0;
        }
        else
        {
            Subscriber data = input.Adapt<Subscriber>();
            _ = _dbContext.Subscribers.Add(data);
            _ = await _dbContext.SaveChangesAsync();

            return 1;
        }
    }
}
