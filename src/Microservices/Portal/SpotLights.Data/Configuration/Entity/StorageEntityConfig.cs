using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Model.Storage;
using SpotLights.Domain.Base;

namespace SpotLights.Data.Configuration.Entity
{
    internal class StorageEntityConfig : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            // Apply BaseEntity configuration
            builder.HasBaseType<BaseEntity>();

            builder.ToTable("Storages");
        }
    }
}
