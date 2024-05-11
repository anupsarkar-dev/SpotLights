using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Dto;
using SpotLights.Domain.Model.Identity;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Model.Storage;

namespace SpotLights.Data.Data;

internal class ApplicationDbContext : IdentityUserContext<UserInfo, int>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<OptionInfo> Options { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<PostCategory> PostCategories { get; set; } = default!;
    public DbSet<Newsletter> Newsletters { get; set; } = default!;
    public DbSet<Subscriber> Subscribers { get; set; } = default!;
    public DbSet<Storage> Storages { get; set; } = default!;

    //public DbSet<StorageReference> StorageReferences { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<UserInfo>(e =>
        {
            _ = e.ToTable("Users");
            _ = e.Property(p => p.Id).HasMaxLength(128);
            _ = e.Property(p => p.CreatedAt).HasColumnOrder(0);
            _ = e.Property(p => p.UpdatedAt).HasColumnOrder(1);
            _ = e.Property(p => p.PasswordHash).HasMaxLength(256);
            _ = e.Property(p => p.SecurityStamp).HasMaxLength(32);
            _ = e.Property(p => p.ConcurrencyStamp).HasMaxLength(64);
            _ = e.Property(p => p.PhoneNumber).HasMaxLength(32);
        });

        _ = modelBuilder.Entity<IdentityUserClaim<int>>(e =>
        {
            _ = e.ToTable("UserClaim");
            _ = e.Property(p => p.ClaimType).HasMaxLength(16);
            _ = e.Property(p => p.ClaimValue).HasMaxLength(256);
        });
        _ = modelBuilder.Entity<IdentityUserLogin<int>>(e =>
        {
            _ = e.ToTable("UserLogin");
            _ = e.Property(p => p.ProviderDisplayName).HasMaxLength(128);
        });
        _ = modelBuilder.Entity<IdentityUserToken<int>>(e =>
        {
            _ = e.ToTable("UserToken");
            _ = e.Property(p => p.Value).HasMaxLength(1024);
        });

        _ = modelBuilder.Entity<OptionInfo>(e =>
        {
            _ = e.ToTable("Options");
            _ = e.HasIndex(b => b.Key).IsUnique();
        });

        _ = modelBuilder.Entity<Post>(e =>
        {
            _ = e.ToTable("Posts");
            _ = e.HasIndex(b => b.Slug).IsUnique();
        });

        _ = modelBuilder.Entity<Storage>(e =>
        {
            _ = e.ToTable("Storages");
        });

        //modelBuilder.Entity<StorageReference>(e =>
        //{
        //  e.ToTable("StorageReferences");
        //  e.HasKey(t => new { t.StorageId, t.EntityId });
        //});

        _ = modelBuilder.Entity<PostCategory>(e =>
        {
            _ = e.ToTable("PostCategories");
            _ = e.HasKey(t => new { t.PostId, t.CategoryId });
        });
    }
}
