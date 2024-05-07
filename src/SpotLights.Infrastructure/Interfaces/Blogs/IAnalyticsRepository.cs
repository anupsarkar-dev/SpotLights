using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Interfaces.Blogs
{
    public interface IAnalyticsRepository : IBaseContextRepository
    {
        Task<(IEnumerable<BlogSumDto> blogs, BarChartViewModel barCharModel)> GetPostSummaryAsync(
            AnalyticsPeriod analyticsPeriod,
            int userId,
            bool isAdmin
        );
    }
}
