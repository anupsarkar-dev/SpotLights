using Microsoft.AspNetCore.Mvc;

namespace SpotLights.Course.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CourseController : ControllerBase
  {
    private readonly ILogger<CourseController> _logger;

    public CourseController(ILogger<CourseController> logger)
    {
      _logger = logger;
    }
  }
}
