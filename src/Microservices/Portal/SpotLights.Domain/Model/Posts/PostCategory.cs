namespace SpotLights.Domain.Model.Posts;

public class PostCategory
{
  public DefaultIdType PostId { get; set; }
  public Post Post { get; set; } = default!;
  public DefaultIdType CategoryId { get; set; }
  public Category Category { get; set; } = default!;
}
