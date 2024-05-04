using Mapster;
using Microsoft.Extensions.DependencyInjection;
using SpotLights.Data.Identity;
using SpotLights.Data.Model.Blogs;
using SpotLights.Data.Repositories.Blogs;
using SpotLights.Data.Repositories.Newsletters;
using SpotLights.Data.Repositories.Posts;
using SpotLights.Options;

namespace SpotLights.Data;

public static class DIExtentions
{
  public static IServiceCollection RegisterRepositories(this IServiceCollection sc)
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

    return sc;
  }
}
