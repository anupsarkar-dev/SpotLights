using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Base;

namespace SpotLights.Data.Configuration.Entity;

internal class PostEntityConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // Apply BaseEntity configuration
        builder.HasBaseType<BaseEntity>();

        builder.ToTable("Posts");
        builder.HasIndex(b => b.Slug).IsUnique();
    }
}
