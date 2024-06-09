using Microsoft.AspNetCore.Mvc;

namespace SpotLights.Course.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class QuizController : ControllerBase
  {
    private readonly ILogger<QuizController> _logger;

    public QuizController(ILogger<QuizController> logger)
    {
      _logger = logger;
    }
  }
}
