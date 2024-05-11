using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Infrastructure.Interfaces.Blogs;
using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Repositories.Blogs;

internal class AnalyticsRepository : BaseContextRepository, IAnalyticsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AnalyticsRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(
        IEnumerable<BlogSumDto> blogs,
        BarChartViewModel barCharModel
    )> GetPostSummaryAsync(AnalyticsPeriod analyticsPeriod, int userId, bool isAdmin)
    {
        DateTime now = DateTime.UtcNow;

        switch (analyticsPeriod)
        {
            case AnalyticsPeriod.Today:
                now = now.Date;
                break;

            case AnalyticsPeriod.Yesterday:
                now = now.AddDays(-1);
                break;

            case AnalyticsPeriod.Days7:
                now = now.AddDays(-7);
                break;

            case AnalyticsPeriod.Days30:
                now = now.AddMonths(-1);
                break;

            case AnalyticsPeriod.Days90:
                now = now.AddMonths(-3);
                break;
        }

        var posts =
            from post in _dbContext.Posts.AsNoTracking()
            where post.State >= PostState.Release && post.PublishedAt >= now
            select post;

        if (!isAdmin)
        {
            posts = posts.Where(s => s.UserId == userId);
        }

        IQueryable<BlogSumDto> result =
            from post in posts.AsNoTracking()
            group post by new
            {
                Time = new
                {
                    post.PublishedAt!.Value.Year,
                    post.PublishedAt!.Value.Month,
                    post.PublishedAt!.Value.Day
                }
            } into g
            select new BlogSumDto
            {
                Time = g.Key.Time.Year + "-" + g.Key.Time.Month + "-" + g.Key.Time.Day,
                Posts = g.Count(m => m.PostType == PostType.Post),
                Pages = g.Count(m => m.PostType == PostType.Page),
                Views = g.Sum(m => m.Views),
            };

        var chartData = await (
            from post in posts
            where post.State >= PostState.Release && post.PublishedAt >= now
            orderby post.Views descending
            select new { post.Title, post.Views }
        )
            .Take(10)
            .ToListAsync();

        BarChartViewModel barChartModel =
            new()
            {
                Labels = chartData.Select(s => s.Title).ToList(),
                Data = chartData.Select(s => s.Views).ToList()
            };

        return (await result.ToListAsync(), barChartModel);
    }

    //public async Task SaveDisplayType(int type)
    //{
    //  var blog = await _dbContext.Blogs.FirstAsync();
    //  blog.AnalyticsListType = type;
    //  await _dbContext.SaveChangesAsync();
    //}

    //public async Task SaveDisplayPeriod(int period)
    //{
    //  var blog = await _dbContext.Blogs.OrderBy(b => b.Id).FirstAsync();
    //  blog.AnalyticsPeriod = period;
    //  await _dbContext.SaveChangesAsync();
    //}
}
