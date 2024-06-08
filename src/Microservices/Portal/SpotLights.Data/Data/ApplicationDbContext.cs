using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpotLights.Data.Configuration.Entity;
using SpotLights.Data.EntityConfiguration;
using SpotLights.Data.ValueGeneration;
using SpotLights.Domain.Dto;
using SpotLights.Domain.Model.Identity;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Model.Storage;
using SpotLights.Shared.Enums;
using System.Reflection.Emit;

namespace SpotLights.Data.Data;

internal class ApplicationDbContext : IdentityUserContext<UserInfo, int>
{
    private readonly DbProvider _provider;

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration)
        : base(options)
    {
        _provider = configuration.GetSection("SpotLights").GetValue<DbProvider>("DbProvider");
    }

    public DbSet<OptionInfo> Options { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<PostCategory> PostCategories { get; set; } = default!;
    public DbSet<Newsletter> Newsletters { get; set; } = default!;
    public DbSet<Subscriber> Subscribers { get; set; } = default!;
    public DbSet<Storage> Storages { get; set; } = default!;

    //public DbSet<StorageReference> StorageReferences { get; set; } = default!;





    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new BaseEntityConfig(_provider));

        builder.ApplyConfiguration(new UserEntityConfig());
        builder.ApplyConfiguration(new IdentityUserEntityConfig());

        builder.Entity<IdentityUserLogin<int>>(e =>
        {
            e.ToTable("UserLogin");
            e.Property(p => p.ProviderDisplayName).HasMaxLength(128);
        });

        builder.Entity<IdentityUserToken<int>>(e =>
        {
            e.ToTable("UserToken");
            e.Property(p => p.Value).HasMaxLength(1024);
        });

        builder.ApplyConfiguration(new PostEntityConfig());
        builder.ApplyConfiguration(new StorageEntityConfig());
        builder.ApplyConfiguration(new PostCategoryEntityConfig());
        builder.ApplyConfiguration(new CategoryEntityConfig());
        builder.ApplyConfiguration(new NewsletterEntityConfig());
        builder.ApplyConfiguration(new SubscriberEntityConfig());
        builder.ApplyConfiguration(new OptionInfoEntityConfig());

        //modelBuilder.Entity<StorageReference>(e =>
        //{
        //  e.ToTable("StorageReferences");
        //  e.HasKey(t => new { t.StorageId, t.EntityId });
        //});
    }
}
