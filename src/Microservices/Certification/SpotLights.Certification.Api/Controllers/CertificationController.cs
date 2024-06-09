using Microsoft.AspNetCore.Mvc;

namespace SpotLights.Certification.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CertificationController : ControllerBase
  {
    private readonly ILogger<CertificationController> _logger;

    public CertificationController(ILogger<CertificationController> logger)
    {
      _logger = logger;
    }
  }
}
