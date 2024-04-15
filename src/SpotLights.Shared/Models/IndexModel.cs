using SpotLights.Shared;
namespace SpotLights.Models;

public class IndexModel : PostPagerModel
{
  public IndexModel(PostPagerDto pager, MainDto main) : base(pager, main)
  {
  }
}
