using Mapster;
using Microsoft.Extensions.DependencyInjection;
using SpotLights.Infrastructure.Identity;
using SpotLights.Infrastructure.Repositories.Blogs;
using SpotLights.Infrastructure.Repositories.Newsletters;
using SpotLights.Infrastructure.Repositories.Posts;
using SpotLights.Infrastructure.Repositories.Options;
using SpotLights.Infrastructure.Provider;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Infrastructure.Repositories.Identity;

namespace SpotLights.Infrastructure;

public static class DIExtentions
{
    public static IServiceCollection AddRepositories(this IServiceCollection sc)
    {
        sc.AddScoped<IMarkdigRepository, MarkdigRepository>();
        sc.AddScoped<IImportRssRepository, ImportRssRepository>();
        sc.AddScoped<IUserRepository, UserRepository>();
        sc.AddScoped<IPostRepository, PostRepository>();
        sc.AddScoped<ICategoryRepository, CategoryRepository>();
        sc.AddScoped<INewsletterRepository, NewsletterProvider>();
        sc.AddScoped<ISubscriberRepository, SubscriberRepository>();

        sc.AddScoped<IOptionRepository, OptionRepository>();
        sc.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
        sc.AddScoped<IEmailRepository, EmailRepository>();
        sc.AddScoped<IImportRepository, ImportRepository>();

        sc.AddScoped<IBlogRepository, BlogRepository>();
        sc.AddScoped<IMainRepository, MainRepository>();

        sc.AddScoped<PostManager>();
        sc.AddScoped<ReverseProvider>();

        return sc;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection sc)
    {
        sc.AddMapster();
        sc.AddIdentity();

        return sc;
    }
}
