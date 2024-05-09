using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpotLights.Shared.Constants;
using SpotLights.Data.Data;
using SpotLights.Infrastructure.Manager.Storages;
using SpotLights.Infrastructure.Interfaces.Storages;

namespace SpotLights.Infrastructure;

public static class StorageExtensions
{
    public static IServiceCollection AddStorageStaticFiles(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IStorageProvider>(sp =>
        {
            ApplicationDbContext dbContext = sp.GetRequiredService<ApplicationDbContext>();
            IConfigurationSection section = configuration.GetSection("SpotLights:Minio");
            bool enable = section.GetValue<bool>("Enable");
            if (enable)
            {
                ILogger<StorageMinioProvider> logger = sp.GetRequiredService<
                    ILogger<StorageMinioProvider>
                >();
                IHttpClientFactory httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                return new StorageMinioProvider(logger, dbContext, httpClientFactory, section);
            }
            else
            {
                ILogger<StorageLocalProvider> logger = sp.GetRequiredService<
                    ILogger<StorageLocalProvider>
                >();
                IHostEnvironment hostEnvironment = sp.GetRequiredService<IHostEnvironment>();
                return new StorageLocalProvider(logger, dbContext, hostEnvironment);
            }
        });
        _ = services.AddScoped<StorageManager>();
        return services;
    }

    public static WebApplication UseStorageStaticFiles(this WebApplication app)
    {
        string fileProviderRoot = Path.Combine(
            app.Environment.ContentRootPath,
            SpotLightsConstant.StorageLocalRoot
        );
        if (!Directory.Exists(fileProviderRoot))
        {
            _ = Directory.CreateDirectory(fileProviderRoot);
        }

        _ = app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fileProviderRoot),
                RequestPath = SpotLightsConstant.StorageLocalPhysicalRoot,
            }
        );
        return app;
    }
}
