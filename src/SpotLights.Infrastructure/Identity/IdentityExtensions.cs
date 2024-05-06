using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Identity;
using SpotLights.Shared.Entities.Identity;

namespace SpotLights.Infrastructure.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        _ = services.AddScoped<UserClaimsPrincipalFactory>();
        _ = services
            .AddIdentityCore<UserInfo>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.ClaimsIdentity.UserIdClaimType = IdentityClaimTypes.UserId;
                options.ClaimsIdentity.UserNameClaimType = IdentityClaimTypes.UserName;
                options.ClaimsIdentity.EmailClaimType = IdentityClaimTypes.Email;
                options.ClaimsIdentity.SecurityStampClaimType = IdentityClaimTypes.SecurityStamp;
            })
            .AddUserManager<UserManager>()
            .AddSignInManager<SignInManager>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();
        _ = services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/account/accessdenied";
            options.LoginPath = "/account/login";
        });

        return services;
    }
}
