using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Data.EntityConfiguration;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Model.Storage;

namespace SpotLights.Data.Configuration.Entity
{
  internal class StorageEntityConfig : IEntityTypeConfiguration<Storage>
  {
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
      // Apply BaseEntity configuration
      BaseEntityConfig.ConfigureBaseEntity<Storage>(builder);

      builder.ToTable("Storages");
    }
  }
}
