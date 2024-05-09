using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotLights.Domain.Model.Identity;

namespace SpotLights.Core.Identity;

public class SignInManager : SignInManager<UserInfo>
{
    public SignInManager(
        UserManager<UserInfo> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<UserInfo> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<UserInfo>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<UserInfo> confirmation
    )
        : base(
            userManager,
            contextAccessor,
            claimsFactory,
            optionsAccessor,
            logger,
            schemes,
            confirmation
        ) { }
}
