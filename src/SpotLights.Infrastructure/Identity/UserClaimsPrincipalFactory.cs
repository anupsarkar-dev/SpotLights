using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SpotLights.Domain.Model.Identity;
using SpotLights.Shared.Entities.Identity;
using System.Security.Claims;

namespace SpotLights.Infrastructure.Identity;

internal class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserInfo>
{
    public UserClaimsPrincipalFactory(
        UserManager<UserInfo> userManager,
        IOptions<IdentityOptions> options
    )
        : base(userManager, options) { }

    public override async Task<ClaimsPrincipal> CreateAsync(UserInfo user)
    {
        ClaimsPrincipal claimsPrincipal = await base.CreateAsync(user);
        ClaimsIdentity id = new("Application");
        id.AddClaim(new Claim(IdentityClaimTypes.NickName, user.NickName));
        id.AddClaim(new Claim(IdentityClaimTypes.Type, ((int)user.Type).ToString()));
        if (!string.IsNullOrEmpty(user.Avatar))
        {
            id.AddClaim(new Claim(IdentityClaimTypes.Avatar, user.Avatar));
        }

        claimsPrincipal.AddIdentity(id);
        return claimsPrincipal;
    }
}
