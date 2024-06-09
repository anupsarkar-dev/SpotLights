using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Data.EntityConfiguration;
using SpotLights.Domain.Model.Posts;

namespace SpotLights.Data.Configuration.Entity;

internal class PostEntityConfig : IEntityTypeConfiguration<Post>
{
  public void Configure(EntityTypeBuilder<Post> builder)
  {
    // Apply BaseEntity configuration
    BaseEntityConfig.ConfigureBaseEntity<Post>(builder);

    builder.ToTable("Posts");
    builder.HasIndex(b => b.Slug).IsUnique();
  }
}
