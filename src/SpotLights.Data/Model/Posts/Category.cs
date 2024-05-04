using System.ComponentModel.DataAnnotations;

namespace SpotLights.Data.Model.Posts;

public class Category : BaseEntity<int>
{
  public DateTime CreatedAt { get; set; }
  [StringLength(120)]
  public string Content { get; set; } = default!;
  [StringLength(255)]
  public string? Description { get; set; } = default!;
  public List<PostCategory>? PostCategories { get; set; }
}
