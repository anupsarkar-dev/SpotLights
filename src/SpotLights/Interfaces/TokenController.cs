using SpotLights.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpotLights.Interfaces;


[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{

  [HttpGet("userinfo")]
  [Authorize]
  public SpotLightsClaims? Get() => SpotLightsClaims.Analysis(User);
}
