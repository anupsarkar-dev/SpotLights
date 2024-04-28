using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotLights.Shared.Identity;

namespace SpotLights.Interfaces;


[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{

  [HttpGet("userinfo")]
  [Authorize]
  public SpotLightsClaims? Get() => SpotLightsClaims.Analysis(User);
}
