using SpotLights.Options;
using SpotLights.Shared;
using SpotLights.Shared.Identity;
using SpotLights.Data.Newsletters;
using SpotLights.Data.Storages;
using Microsoft.EntityFrameworkCore;

namespace SpotLights.Data;

public class MySqlDbContext : AppDbContext
{
  public MySqlDbContext(DbContextOptions options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);


    modelBuilder.Entity<UserInfo>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });
    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Post>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Category>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Newsletter>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Subscriber>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Storage>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    });

    //modelBuilder.Entity<StorageReference>(e =>
    //{
    //  e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    //});
  }
}
