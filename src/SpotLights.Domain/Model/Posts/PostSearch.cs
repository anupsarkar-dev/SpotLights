using SpotLights.Domain;
using SpotLights.Shared;

namespace SpotLights.Domain.Model.Posts;

public class PostSearch
{
  public PostSearch(PostItemDto post, int rank)
  {
    Post = post;
    Rank = rank;
  }
  public PostItemDto Post { get; set; }
  public int Rank { get; set; }
}
