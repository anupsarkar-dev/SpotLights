using SpotLights.Certificaion.Infrastructure.Data;
using SpotLights.Certification.Infrastructure.Interfaces;
using SpotLights.Common.Library.Base;

namespace SpotLights.Certification.Infrastructure.Repositories
{
  public class CertificatationRepository
    : Repository<CertificateDbContext>,
      ICertificationRepository
  {
    public CertificatationRepository(CertificateDbContext context)
      : base(context) { }
  }
}
