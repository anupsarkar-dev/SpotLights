namespace SpotLights.Certification.Domain.Dto;

public class CertificateDto
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int CourseId { get; set; }
  public DateTime IssuedAt { get; set; }
  public string CertificateUrl { get; set; } = string.Empty;
}
