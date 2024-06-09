using SpotLights.Common.Library.Base;

namespace SpotLights.Certification.Domain.Model;

public class Certificate : BaseEntity
{
  public int UserId { get; set; }
  public int CourseId { get; set; }
  public DateTime IssuedAt { get; set; }
  public string CertificateUrl { get; set; } = string.Empty;
}
