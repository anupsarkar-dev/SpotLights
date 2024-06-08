using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Model.Posts;


namespace SpotLights.Data.Configuration.Entity
{
    internal class PostCategoryEntityConfig : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.ToTable("PostCategories");
            builder.HasKey(t => new { t.PostId, t.CategoryId });
        }
    }
}
