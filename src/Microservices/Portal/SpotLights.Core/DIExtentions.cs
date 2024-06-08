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
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Core.Provider;
using SpotLights.Core.Interfaces.Provider;

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

    public static IServiceCollection AddProviders(this IServiceCollection sc)
    {
        sc.AddScoped<IIdentityService, IdentityService>();
        sc.AddScoped<IStorageProvider, StorageProvider>();
        return sc;
    }

    public static IServiceCollection AddCore(this IServiceCollection sc)
    {
        return sc;
    }
}
