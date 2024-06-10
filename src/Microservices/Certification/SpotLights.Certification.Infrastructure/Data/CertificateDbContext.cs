using Microsoft.EntityFrameworkCore;
using SpotLights.Certification.Infrastructure.Data.EntityConfiguration;

namespace SpotLights.Certificaion.Infrastructure.Data
{
  public class CertificateDbContext : DbContext
  {
    public CertificateDbContext(DbContextOptions options)
      : base(options) { }

    public DbSet<Certification.Domain.Model.Certificate> Certificates { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfiguration(new CertificateEntityConfig());
    }
  }
}
