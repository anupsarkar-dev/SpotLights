using System.ComponentModel.DataAnnotations;

namespace SpotLights.Shared;

public class AccountProfilePasswordModel : AccountProfileModel
{
  public string? Error { get; set; }
  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;
  [Required]
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "Passwords do not match")]
  public string PasswordConfirm { get; set; } = default!;
}
