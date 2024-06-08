using SpotLights.Shared.Entities.Identity;
using System;
using System.Security.Claims;

namespace SpotLights.Shared.Extensions;

public static class PrincipalExtensions
{
    public static string FirstValue(this ClaimsPrincipal principal, string claimType)
    {
        string? value = FirstOrDefault(principal, claimType);
        return value == null ? throw new NullReferenceException(nameof(value)) : value;
    }

    public static string? FirstOrDefault(this ClaimsPrincipal principal, string claimType)
    {
        return principal.FindFirstValue(claimType);
    }

    public static DefaultIdType FirstUserId(this ClaimsPrincipal principal)
    {
        string userIdString = FirstValue(principal, IdentityClaimTypes.UserId);

        if (typeof(DefaultIdType) == typeof(int))
        {
            return int.Parse(userIdString);
        }

        //if (typeof(DefaultIdType) == typeof(Guid))
        //{
        //    return Guid.Parse(userIdString);
        //}

        throw new InvalidOperationException("Invalid DefaultIdType!");
    }

    public static bool IsAdmin(this ClaimsPrincipal principal)
    {
        string userType = FirstValue(principal, IdentityClaimTypes.Type);
        return userType == UserType.Administrator.ToString("D");
    }
}
