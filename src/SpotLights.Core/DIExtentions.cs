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
        sc.AddScoped<INewsletterService, INewsletterService>();
        sc.AddScoped<Core.Interfaces.ISubscriberService, SubscriberService>();

        sc.AddScoped<IOptionService, OptionService>();
        sc.AddScoped<IAnalyticsService, AnalyticsService>();
        sc.AddScoped<Core.Interfaces.Newsletter.IEmailService, EmailService>();
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
