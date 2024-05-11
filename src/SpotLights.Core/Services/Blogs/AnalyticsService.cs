using SpotLights.Core.Interfaces.Blogs;
using SpotLights.Infrastructure.Interfaces.Blogs;
using SpotLights.Shared;
using SpotLights.Shared.Enums;

namespace SpotLights.Core.Services.Blogs;

internal class AnalyticsService : IAnalyticsService
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
