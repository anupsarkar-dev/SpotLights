using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Data.EntityConfiguration;

namespace SpotLights.Certification.Infrastructure.Data.EntityConfiguration
{
  internal class CertificateEntityConfig : IEntityTypeConfiguration<Domain.Model.Certificate>
  {
    public void Configure(EntityTypeBuilder<Domain.Model.Certificate> builder)
    {
      // Apply BaseEntity configuration
      BaseEntityConfig.ConfigureBaseEntity(builder);

      builder.ToTable(nameof(Domain.Model.Certificate));
    }
  }
}
