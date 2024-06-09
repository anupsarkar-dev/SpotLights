using Microsoft.AspNetCore.Mvc;

namespace SpotLights.Certification.Api.Controllers
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
