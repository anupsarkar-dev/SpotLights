using SpotLights.Data.Model.Posts;
using System;

namespace SpotLights.Data.Model.Newsletters;

public class Newsletter : BaseEntity<int>
{
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int PostId { get; set; }
  public bool Success { get; set; }
  public Post Post { get; set; } = default!;
}
