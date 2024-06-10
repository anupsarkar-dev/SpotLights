using SpotLights.Certification.Core.Interfaces;
using SpotLights.Certification.Domain.Model;
using SpotLights.Certification.Infrastructure.Interfaces;
using SpotLights.Common.Library.Base;
using SpotLights.Common.Library.Interfaces;

namespace SpotLights.Certification.Core.Services
{
  public class CertificationService : Service<Certificate>, ICertificationService
  {
    public CertificationService(ICertificationRepository repository)
      : base(repository) { }
  }
}
