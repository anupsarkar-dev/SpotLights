using Microsoft.AspNetCore.Identity;
using SpotLights.Shared.Entities.Identity;
using SpotLights.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpotLights.Domain.Model.Identity;

public class UserInfo : IdentityUser<DefaultIdType>
{
    public UserInfo()
        : base() { }

    public UserInfo(string userName)
        : base()
    {
        UserName = userName;
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [StringLength(256)]
    public string NickName { get; set; } = default!;

    [StringLength(1024)]
    public string? Avatar { get; set; }

    [StringLength(2048)]
    public string? Bio { get; set; }

    [StringLength(32)]
    public string? Gender { get; set; }

    public UserType Type { get; set; }
    public UserState State { get; set; }
}
