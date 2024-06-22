using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SpotLights.Routing.Gateway.Extension;

public static class WebAppBuilderExtensions
{
  public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
  {
    builder
      .Services.AddAuthentication(config =>
      {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(option =>
      {
        option.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
              builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Secret")
            )
          ),
          ValidateIssuer = true,
          ValidIssuer = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Issuer"),
          ValidateAudience = true,
          ValidAudience = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Audience")
        };
      });
    return builder;
  }
}
