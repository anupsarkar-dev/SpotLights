using SpotLights.Shared.Identity;
using System;
using System.Security.Claims;

namespace SpotLights.Shared.Extensions;

public static class PrincipalExtensions
{
  public static string FirstValue(this ClaimsPrincipal principal, string claimType)
  {
    var value = FirstOrDefault(principal, claimType);
    if (value == null) throw new NullReferenceException(nameof(value));
    return value;
  }

  public static string? FirstOrDefault(this ClaimsPrincipal principal, string claimType)
    => principal.FindFirstValue(claimType);


  public static int FirstUserId(this ClaimsPrincipal principal)
  {
    var userIdString = FirstValue(principal, SpotLightsClaimTypes.UserId);
    return int.Parse(userIdString);
  }

  public static bool IsAdmin(this ClaimsPrincipal principal)
  {
    var userType = FirstValue(principal, SpotLightsClaimTypes.Type);
    return userType == UserType.Administrator.ToString("D");
  }
}
