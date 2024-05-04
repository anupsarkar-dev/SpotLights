using System.ComponentModel.DataAnnotations;

namespace SpotLights.Shared;

public class AccountInitializeViewModel : AccountRegisterViewModel
{
  [Required]
  public string Title { get; set; } = default!;
  [Required]
  public string Description { get; set; } = default!;
}
