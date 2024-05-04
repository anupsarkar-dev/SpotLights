using Mapster;
using Microsoft.Extensions.DependencyInjection;
using SpotLights.Infrastructure.Identity;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Infrastructure.Repositories.Newsletters;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Infrastructure.Repositories.Options;

namespace SpotLights.Infrastructure;

public static class DIExtentions
{
    public static IServiceCollection AddRepositories(this IServiceCollection sc)
    {
        sc.AddScoped<MarkdigProvider>();
        sc.AddScoped<ReverseProvider>();
        sc.AddScoped<ImportRssProvider>();
        sc.AddScoped<UserProvider>();
        sc.AddScoped<PostProvider>();
        sc.AddScoped<CategoryProvider>();
        sc.AddScoped<NewsletterProvider>();
        sc.AddScoped<SubscriberProvider>();

        sc.AddScoped<OptionProvider>();
        sc.AddScoped<AnalyticsProvider>();
        sc.AddScoped<EmailManager>();
        sc.AddScoped<ImportManager>();
        sc.AddScoped<PostManager>();
        sc.AddScoped<BlogManager>();
        sc.AddScoped<MainManager>();

        return sc;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection sc)
    {
        sc.AddMapster();
        sc.AddIdentity();

        return sc;
    }
}
