using SpotLights.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpotLights.Domain.Model.Newsletters;

public class Subscriber : BaseEntity
{
    [EmailAddress]
    [StringLength(160)]
    public string Email { get; set; } = default!;

    [StringLength(80)]
    public string? Ip { get; set; }

    [StringLength(120)]
    public string? Country { get; set; }

    [StringLength(120)]
    public string? Region { get; set; }
}
