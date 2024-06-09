using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Common.Library.Enums;
using SpotLights.Common.Library.Utils;

namespace SpotLights.Data.EntityConfiguration;

public static class BaseEntityConfig
{
  public static void ConfigureBaseEntity<T>(
    EntityTypeBuilder<T> builder,
    DbProvider provider = DbProvider.Mssql
  )
    where T : BaseEntity
  {
    switch (provider)
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
