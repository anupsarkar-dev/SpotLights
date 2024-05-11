using SpotLights.Data.ValueGeneration;
using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Identity;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Domain.Model.Storage;
using SpotLights.Domain.Dto;

namespace SpotLights.Data;

internal class SqlServerDbContext : ApplicationDbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserInfo>(e =>
        {
            e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");

            // https://github.com/dotnet/EntityFramework.Docs/issues/3057
            // https://github.com/dotnet/efcore/issues/19765
            // TOTO No solution has been found
            // This configuration is not updated when the entity is updated
            e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });
        modelBuilder.Entity<OptionInfo>(e =>
        {
            e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        modelBuilder.Entity<Post>(e =>
        {
            e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        modelBuilder.Entity<Category>(e =>
        {
            e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
        });

        modelBuilder.Entity<Newsletter>(e =>
        {
            e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        modelBuilder.Entity<Subscriber>(e =>
        {
            e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            e.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));
        });

        modelBuilder.Entity<Storage>(e =>
        {
            e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
        });

        //modelBuilder.Entity<StorageReference>(e =>
        //{
        //  e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
        //});
    }
}
