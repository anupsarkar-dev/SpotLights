using SpotLights.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace SpotLights.Domain.Model.Posts;

public class Category : BaseEntity
{
    public DateTime CreatedAt { get; set; }

    [StringLength(120)]
    public string Content { get; set; } = default!;

    [StringLength(255)]
    public string? Description { get; set; } = default!;
    public bool IsShowInHomePage { get; set; }
    public List<PostCategory>? PostCategories { get; set; }
}
