using System.Xml.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotLights.Common.Library.Enums;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Posts;

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
    var provider = section.GetValue<DbProvider>("DbProvider");
    var connectionString = section.GetValue<string>("ConnString");

    if (provider == DbProvider.Sqlite)
    {
      var sonnectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);
      var dataSourcePath = Path.Combine(
        environment.ContentRootPath,
        sonnectionStringBuilder.DataSource
      );

      var dataSourceDirectory = Path.GetDirectoryName(dataSourcePath);

      if (!string.IsNullOrEmpty(dataSourceDirectory) && !Directory.Exists(dataSourceDirectory))
      {
        Directory.CreateDirectory(dataSourceDirectory);
      }

      services.AddDbContext<ApplicationDbContext>(o =>
        o.UseSqlite(sonnectionStringBuilder.ToString())
      );
    }
    else if (provider == DbProvider.Mssql)
    {
      services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
    }
    else if (provider == DbProvider.Postgres)
    {
      services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
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
      var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

      if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
      {
        await dbContext.Database.MigrateAsync();
      }

      SeedData(dbContext);
    }
    return app;
  }

  private static void SeedData(ApplicationDbContext dbContext)
  {
    SeedCategories(dbContext);
  }

  private static void SeedCategories(ApplicationDbContext dbContext)
  {
    var categories = new List<Category>
    {
      new Category("Programming"),
      new Category("C#"),
      new Category(".NET"),
    };

    dbContext.AddRange(categories);
    dbContext.SaveChanges();
  }
}
