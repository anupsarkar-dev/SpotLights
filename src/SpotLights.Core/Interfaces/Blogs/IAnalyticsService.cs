using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Core.Interfaces.Blogs
{
    internal interface IAnalyticsService
    {
        Task<(IEnumerable<BlogSumDto> blogs, BarChartViewModel barCharModel)> GetPostSummaryAsync(
            AnalyticsPeriod analyticsPeriod,
            int userId,
            bool isAdmin
        );
    }
}
