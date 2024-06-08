using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Data.ValueGeneration;
using SpotLights.Shared.Enums;

namespace SpotLights.Data.EntityConfiguration;

internal class BaseEntityConfig : IEntityTypeConfiguration<BaseEntity>
{
  private readonly DbProvider _provider;

  public BaseEntityConfig(DbProvider provider)
  {
    _provider = provider;
  }

  public void Configure(EntityTypeBuilder<BaseEntity> builder)
  {
    switch (_provider)
    {
      case DbProvider.Mssql:
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");

        break;
      case DbProvider.Postgres:
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("now()");

        break;
      case DbProvider.Sqlite:
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("datetime()");
        break;
      default:
        break;
    }

    builder.Property(b => b.UpdatedAt).HasValueGenerator(typeof(DateTimetValueGenerator));

    builder.Property(b => b.CreatedBy).HasMaxLength(128).IsRequired(false);

    builder.Property(b => b.IsDeleted).HasDefaultValue(false);
  }
}
