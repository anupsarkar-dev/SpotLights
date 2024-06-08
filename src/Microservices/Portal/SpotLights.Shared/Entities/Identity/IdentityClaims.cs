using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SpotLights.Shared.Entities.Identity;

public class IdentityClaims
{
  public string UserId { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Email { get; set; }
  public string? Avatar { get; set; }
  public UserType Type { get; set; }
  public static IdentityClaims? Analysis(ClaimsPrincipal principal)
  {
    if (principal.Identity == null || !principal.Identity.IsAuthenticated)
    {
      return null;
    }

    IdentityClaims user = new();
    foreach (Claim claim in principal.Claims)
    {
      switch (claim.Type)
      {
        case IdentityClaimTypes.UserId:
          user.UserId = claim.Value; break;
        case IdentityClaimTypes.UserName:
          user.UserName = claim.Value; break;
        case IdentityClaimTypes.Email:
          user.Email = claim.Value; break;
        case IdentityClaimTypes.NickName:
          user.NickName = claim.Value; break;
        case IdentityClaimTypes.Avatar:
          user.Avatar = claim.Value; break;
        case IdentityClaimTypes.Type:
          user.Type = (UserType)Enum.Parse(typeof(UserType), claim.Value); break;
        default:
          {
            break;
          }
      }
    }
    return user;
  }

  public static ClaimsPrincipal Generate(IdentityClaims? identity)
  {
    if (identity != null)
    {
      List<Claim> claims = new()
      {
        new Claim(ClaimTypes.Name, identity.UserName),
        new Claim(IdentityClaimTypes.UserId, identity.UserId),
        new Claim(IdentityClaimTypes.UserName, identity.UserName),
        new Claim(IdentityClaimTypes.NickName, identity.NickName),
        new Claim(IdentityClaimTypes.Type, ((int)identity.Type).ToString()),
      };
      if (!string.IsNullOrEmpty(identity.Email))
      {
        claims.Add(new Claim(IdentityClaimTypes.Email, identity.Email));
      }

      return new ClaimsPrincipal(new ClaimsIdentity(claims, "identity"));
    }
    return new ClaimsPrincipal(new ClaimsIdentity());
  }
}
