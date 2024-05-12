using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotLights.Shared.Entities.Identity;

namespace SpotLights.Interfaces;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    [HttpGet("userinfo")]
    [Authorize]
    public IdentityClaims? Get() => IdentityClaims.Analysis(User);
}
