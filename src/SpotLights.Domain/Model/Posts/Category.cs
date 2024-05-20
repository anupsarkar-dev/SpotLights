using SpotLights.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace SpotLights.Domain.Model.Posts;

public class Category : BaseEntity
{
    [StringLength(120)]
    public string Content { get; set; } = default!;

    [StringLength(255)]
    public string? Description { get; set; } = default!;
    public List<PostCategory>? PostCategories { get; set; }

    public bool ShowInMenu { get; set; }
}
