using SpotLights.Shared;
namespace SpotLights.Models;

public class IndexViewModel : PostPagerModel
{
  public IndexViewModel(PostPagerDto pager, MainDto main) : base(pager, main)
  {
  }
}
