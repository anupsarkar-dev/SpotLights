using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotLights.Data.Data;

namespace SpotLights.Data;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration
    )
    {
        var section = configuration.GetSection("SpotLights");
        var provider = section.GetValue<string>("DbProvider");
        var connectionString = section.GetValue<string>("ConnString");

        if ("Sqlite".Equals(provider, StringComparison.OrdinalIgnoreCase))
        {
            var sonnectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);
            var dataSourcePath = Path.Combine(
                environment.ContentRootPath,
                sonnectionStringBuilder.DataSource
            );
            var dataSourceDirectory = Path.GetDirectoryName(dataSourcePath);
            if (
                !string.IsNullOrEmpty(dataSourceDirectory) && !Directory.Exists(dataSourceDirectory)
            )
                Directory.CreateDirectory(dataSourceDirectory);
            services.AddDbContext<AppDbContext, SqliteDbContext>(
                o => o.UseSqlite(sonnectionStringBuilder.ToString())
            );
        }
        else if ("SqlServer".Equals(provider, StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<AppDbContext, SqlServerDbContext>(
                o => o.UseSqlServer(connectionString)
            );
        }
        else if ("Postgres".Equals(provider, StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<AppDbContext, PostgresDbContext>(
                o => o.UseNpgsql(connectionString)
            );
        }
        else
        {
            throw new Exception($"Unsupported provider: {provider}");
        }

        if (environment.IsDevelopment())
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
        }
        return services;
    }

    public static async Task<WebApplication> RunDbContextMigrateAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }
        }
        return app;
    }
}
