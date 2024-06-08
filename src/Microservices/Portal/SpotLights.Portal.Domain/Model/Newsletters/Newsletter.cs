using SpotLights.Common.Library.Base;
using SpotLights.Domain.Model.Posts;

namespace SpotLights.Domain.Model.Newsletters;

public class Newsletter : BaseEntity
{
  public int PostId { get; set; }
  public bool Success { get; set; }
  public Post Post { get; set; } = default!;
}
