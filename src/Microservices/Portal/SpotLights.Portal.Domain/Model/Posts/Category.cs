using System.ComponentModel.DataAnnotations;
using SpotLights.Common.Library.Base;

namespace SpotLights.Domain.Model.Posts;

public class Category : BaseEntity
{
  public Category() { }

  public Category(string content, string? description = null, bool showInMenu = false)
  {
    Content = content;
    Description = description;
    ShowInMenu = showInMenu;
  }

  [StringLength(120)]
  public string Content { get; set; } = default!;

  [StringLength(255)]
  public string? Description { get; set; }
  public List<PostCategory>? PostCategories { get; set; }
  public bool ShowInMenu { get; set; }
}
