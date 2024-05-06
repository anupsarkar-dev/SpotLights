using Microsoft.Extensions.DependencyInjection;
using SpotLights.Infrastructure.Identity;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Infrastructure.Repositories.Newsletters;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Infrastructure.Repositories.Options;
using SpotLights.Core.Interfaces;

namespace SpotLights.Infrastructure;

public static class DIExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection sc)
    {
        sc.AddScoped<IMarkdigService, MarkdigService>();
        sc.AddScoped<IImportRssService, ImportRssService>();
        sc.AddScoped<IUserService, UserService>();
        sc.AddScoped<IPostService, PostService>();
        sc.AddScoped<ICategoryService, CategoryService>();
        sc.AddScoped<INewsletterService, NewsletterService>();
        sc.AddScoped<ISubscriberService, SubscriberService>();

        sc.AddScoped<IOptionService, OptionService>();
        sc.AddScoped<IAnalyticsService, AnalyticsService>();
        sc.AddScoped<IEmailService, EmailService>();
        sc.AddScoped<IImportService, ImportService>();

        sc.AddScoped<IBlogService, BlogService>();
        sc.AddScoped<IMainService, MainService>();

        return sc;
    }

    public static IServiceCollection AddCore(this IServiceCollection sc)
    {
        return sc;
    }
}
