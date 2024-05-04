using SpotLights.Data.ValueGeneration;
using SpotLights.Options;
using Microsoft.EntityFrameworkCore;
using SpotLights.Shared.Identity;
using SpotLights.Data.Model.Posts;
using SpotLights.Data.Model.Newsletters;
using SpotLights.Data.Manager.Storages;

namespace SpotLights.Data;

public class SqlServerDbContext : AppDbContext
{
  public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
  {
  }

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
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });
    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });

    modelBuilder.Entity<Post>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });

    modelBuilder.Entity<Category>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
    });

    modelBuilder.Entity<Newsletter>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });

    modelBuilder.Entity<Subscriber>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
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
