using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Common.Library.Utils;

namespace SpotLights.Data.EntityConfiguration;

internal class BaseEntityConfig : IEntityTypeConfiguration<BaseEntity>
{
  public BaseEntityConfig() { }

  public void Configure(EntityTypeBuilder<BaseEntity> builder)
  {
    builder.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");

    builder.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));

    builder.Property(b => b.CreatedBy).HasMaxLength(128).IsRequired(false);

    builder.Property(b => b.IsDeleted).HasDefaultValue(false);
  }
}
