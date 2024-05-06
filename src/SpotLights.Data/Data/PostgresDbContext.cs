using Microsoft.EntityFrameworkCore;
using SpotLights.Data.ValueGeneration;
using SpotLights.Domain.Dto;
using SpotLights.Domain.Model.Identity;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Model.Storage;

namespace SpotLights.Data.Data;

public class PostgresDbContext : ApplicationDbContext
{
    public PostgresDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<UserInfo>(e =>
        {
            _ = e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");

            // https://github.com/dotnet/EntityFramework.Docs/issues/3057
            // https://github.com/dotnet/efcore/issues/19765
            // TOTO No solution has been found
            // This configuration is not updated when the entity is updated
            _ = e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });
        _ = modelBuilder.Entity<OptionInfo>(e =>
        {
            _ = e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");
            _ = e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        _ = modelBuilder.Entity<Post>(e =>
        {
            _ = e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");
            _ = e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        _ = modelBuilder.Entity<Category>(e =>
        {
            _ = e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");
        });

        _ = modelBuilder.Entity<Newsletter>(e =>
        {
            _ = e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");
            _ = e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        _ = modelBuilder.Entity<Subscriber>(e =>
        {
            _ = e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");
            _ = e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        _ = modelBuilder.Entity<Storage>(e =>
        {
            _ = e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");
        });

        //modelBuilder.Entity<StorageReference>(e =>
        //{
        //  e.Property(b => b.CreatedAt).HasDefaultValueSql("now()");
        //});
    }
}
