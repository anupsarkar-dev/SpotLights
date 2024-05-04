using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SpotLights.Shared.Identity;
using System.Security.Claims;

namespace SpotLights.Data.Identity;

public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserInfo>
{
  public UserClaimsPrincipalFactory(
    UserManager<UserInfo> userManager,
    IOptions<IdentityOptions> options)
    : base(userManager, options)
  {
  }

  public override async Task<ClaimsPrincipal> CreateAsync(UserInfo user)
  {
    ClaimsPrincipal claimsPrincipal = await base.CreateAsync(user);
    ClaimsIdentity id = new("Application");
    id.AddClaim(new Claim(SpotLightsClaimTypes.NickName, user.NickName));
    id.AddClaim(new Claim(SpotLightsClaimTypes.Type, ((int)user.Type).ToString()));
    if (!string.IsNullOrEmpty(user.Avatar))
    {
      id.AddClaim(new Claim(SpotLightsClaimTypes.Avatar, user.Avatar));
    }

    claimsPrincipal.AddIdentity(id);
    return claimsPrincipal;
  }
}
