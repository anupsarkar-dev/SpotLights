using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface IAnalyticsRepository
    {
        Task<(IEnumerable<BlogSumDto> blogs, BarChartViewModel barCharModel)> GetPostSummaryAsync(
            AnalyticsPeriod analyticsPeriod,
            int userId,
            bool isAdmin
        );
    }
}
