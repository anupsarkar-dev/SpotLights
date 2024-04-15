using SpotLights.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SpotLights.Identity;

public static class IdentityExtensions
{
  public static IServiceCollection AddIdentity(this IServiceCollection services)
  {
    services.AddScoped<UserClaimsPrincipalFactory>();
    services.AddIdentityCore<UserInfo>(options =>
    {
      options.User.RequireUniqueEmail = true;
      options.Password.RequireUppercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.ClaimsIdentity.UserIdClaimType = SpotLightsClaimTypes.UserId;
      options.ClaimsIdentity.UserNameClaimType = SpotLightsClaimTypes.UserName;
      options.ClaimsIdentity.EmailClaimType = SpotLightsClaimTypes.Email;
      options.ClaimsIdentity.SecurityStampClaimType = SpotLightsClaimTypes.SecurityStamp;
    }).AddUserManager<UserManager>()
      .AddSignInManager<SignInManager>()
      .AddEntityFrameworkStores<AppDbContext>()
      .AddDefaultTokenProviders()
      .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();
    services.ConfigureApplicationCookie(options =>
    {
      options.AccessDeniedPath = "/account/accessdenied";
      options.LoginPath = "/account/login";
    });
    return services;
  }
}
