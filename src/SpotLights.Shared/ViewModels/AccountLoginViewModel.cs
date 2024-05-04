using System.ComponentModel.DataAnnotations;

namespace SpotLights.Shared;

public class AccountLoginViewModel : AccountViewModel
{
  public bool ShowError { get; set; }
  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;
}
