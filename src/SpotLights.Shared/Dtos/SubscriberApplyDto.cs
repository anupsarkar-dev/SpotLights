using System.ComponentModel.DataAnnotations;

namespace SpotLights.Shared;

public class SubscriberApplyDto
{
  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;
}
