using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Base;

namespace SpotLights.Data.Configuration.Entity
{
    internal class CategoryEntityConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Apply BaseEntity configuration
            builder.HasBaseType<BaseEntity>();
        }
    }
}
