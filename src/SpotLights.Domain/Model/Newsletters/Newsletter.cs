using SpotLights.Domain.Model.Posts;
using System;

namespace SpotLights.Domain.Model.Newsletters;

public class Newsletter : BaseEntity<int>
{
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int PostId { get; set; }
  public bool Success { get; set; }
  public Post Post { get; set; } = default!;
}
