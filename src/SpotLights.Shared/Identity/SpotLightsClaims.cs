using SpotLights.Shared;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SpotLights.Shared.Identity;

public class SpotLightsClaims
{
  public string UserId { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Email { get; set; }
  public string? Avatar { get; set; }
  public UserType Type { get; set; }
  public static SpotLightsClaims? Analysis(ClaimsPrincipal principal)
  {
    if (principal.Identity == null || !principal.Identity.IsAuthenticated)
      return null;

    var user = new SpotLightsClaims();
    foreach (var claim in principal.Claims)
    {
      switch (claim.Type)
      {
        case SpotLightsClaimTypes.UserId:
          user.UserId = claim.Value; break;
        case SpotLightsClaimTypes.UserName:
          user.UserName = claim.Value; break;
        case SpotLightsClaimTypes.Email:
          user.Email = claim.Value; break;
        case SpotLightsClaimTypes.NickName:
          user.NickName = claim.Value; break;
        case SpotLightsClaimTypes.Avatar:
          user.Avatar = claim.Value; break;
        case SpotLightsClaimTypes.Type:
          user.Type = (UserType)Enum.Parse(typeof(UserType), claim.Value); break;
        default:
          {
            break;
          }
      }
    }
    return user;
  }

  public static ClaimsPrincipal Generate(SpotLightsClaims? identity)
  {
    if (identity != null)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, identity.UserName),
        new Claim(SpotLightsClaimTypes.UserId, identity.UserId),
        new Claim(SpotLightsClaimTypes.UserName, identity.UserName),
        new Claim(SpotLightsClaimTypes.NickName, identity.NickName),
        new Claim(SpotLightsClaimTypes.Type, ((int)identity.Type).ToString()),
      };
      if (!string.IsNullOrEmpty(identity.Email)) claims.Add(new Claim(SpotLightsClaimTypes.Email, identity.Email));
      return new ClaimsPrincipal(new ClaimsIdentity(claims, "identity"));
    }
    return new ClaimsPrincipal(new ClaimsIdentity());
  }
}
