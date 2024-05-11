using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotLights.Shared.Entities.Identity;

namespace SpotLights.Interfaces;

[Route("api/token")]
[ApiController]
internal class TokenController : ControllerBase
{
    [HttpGet("userinfo")]
    [Authorize]
    public IdentityClaims? Get() => IdentityClaims.Analysis(User);
}
