using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces.Blogs;
using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Infrastructure.Repositories.Blogs;

public class AnalyticsService : IAnalyticsService
{
    private readonly IAnalyticsRepository _analyticsRepository;

    public AnalyticsService(IAnalyticsRepository analyticsRepository)
    {
        _analyticsRepository = analyticsRepository;
    }

    public async Task<(
        IEnumerable<BlogSumDto> blogs,
        BarChartViewModel barCharModel
    )> GetPostSummaryAsync(AnalyticsPeriod analyticsPeriod, int userId, bool isAdmin)
    {
        return await _analyticsRepository.GetPostSummaryAsync(analyticsPeriod, userId, isAdmin);
    }
}
