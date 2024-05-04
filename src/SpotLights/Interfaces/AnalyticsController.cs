using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SpotLights.Shared.Extensions;
using SpotLights.Data.Model.Blogs;

namespace SpotLights.Interfaces;

[Route("api/analytics")]
[ApiController]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsProvider _analyticsProvider;

    public AnalyticsController(AnalyticsProvider analyticsProvider)
    {
        _analyticsProvider = analyticsProvider;
    }

    [HttpGet("{typeId:int}/{periodId:int}")]
    public async Task<AnalyticsDto> GetAnalytics(int typeId, int periodId)
    {
        var userId = User.FirstUserId();
        var isAdmin = User.IsAdmin();

        var result = await _analyticsProvider.GetPostSummaryAsync(
            (AnalyticsPeriod)periodId,
            userId,
            isAdmin
        );

        var blogs = result.blogs;

        return new AnalyticsDto
        {
            Blogs = blogs,
            TotalPosts = blogs.Sum(s => s.Posts),
            TotalPages = blogs.Sum(s => s.Pages),
            TotalViews = blogs.Sum(s => s.Views),
            TotalSubscribers = blogs.Sum(s => s.Subscribers),
            DisplayType = (AnalyticsListType)typeId,
            DisplayPeriod = (AnalyticsPeriod)periodId,
            LatestPostViews = result.barCharModel
        };
    }

    //[HttpPut("displayType/{typeId:int}")]
    //public async Task SaveDisplayType(int typeId)
    //{
    //  await _analyticsProvider.SaveDisplayType(typeId);
    //}

    //[HttpPut("displayPeriod/{typeId:int}")]
    //public async Task SaveDisplayPeriod(int typeId)
    //{
    //  await _analyticsProvider.SaveDisplayPeriod(typeId);
    //}
}
