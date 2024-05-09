using Microsoft.Extensions.DependencyInjection;
using SpotLights.Infrastructure.Repositories.Newsletters;
using SpotLights.Core.Interfaces;
using SpotLights.Core.Services.Blogs;
using SpotLights.Core.Services.Identity;
using SpotLights.Core.Services.Options;
using SpotLights.Core.Services.Posts;
using SpotLights.Core.Interfaces.Blogs;
using SpotLights.Core.Interfaces.Post;
using SpotLights.Core.Interfaces.Newsletter;
using SpotLights.Core.Interfaces.Identity;
using SpotLights.Core.Interfaces.Options;
using Mapster;
using SpotLights.Core.Identity;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Identity;
using SpotLights.Shared.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using SpotLights.Infrastructure.Repositories.Posts;

namespace SpotLights.Infrastructure;

public static class DIExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection sc)
    {
        sc.AddScoped<IMarkdigService, MarkdigService>();
        sc.AddScoped<IImportRssService, ImportRssService>();
        sc.AddScoped<IUserService, UserService>();
        sc.AddScoped<IPostService, PostService>();
        sc.AddScoped<IPostProviderService, PostProviderService>();
        sc.AddScoped<ICategoryService, CategoryService>();
        sc.AddScoped<INewsletterService, NewsletterService>();
        sc.AddScoped<ISubscriberService, SubscriberService>();

        sc.AddScoped<IOptionService, OptionService>();
        sc.AddScoped<IAnalyticsService, AnalyticsService>();
        sc.AddScoped<IImportService, ImportService>();

        sc.AddScoped<IBlogService, BlogService>();
        sc.AddScoped<IMainService, MainService>();
        sc.AddScoped<IEmailsService, EmailsService>();

        return sc;
    }

    public static IServiceCollection AddCore(this IServiceCollection sc)
    {
        return sc;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddScoped<UserClaimsPrincipalFactory>();
        services
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
            .AddUserManager<UsersManager>()
            .AddSignInManager<SignInManager>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
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
