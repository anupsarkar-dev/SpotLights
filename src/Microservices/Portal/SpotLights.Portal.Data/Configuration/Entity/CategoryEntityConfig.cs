using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Data.EntityConfiguration;
using SpotLights.Domain.Model.Posts;

namespace SpotLights.Data.Configuration.Entity
{
  internal class CategoryEntityConfig : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      // Apply BaseEntity configuration
      BaseEntityConfig.ConfigureBaseEntity<Category>(builder);
    }
  }
}
