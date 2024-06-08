namespace SpotLights.Shared;

public class PostPagerModel : MainViewModel
{
  public PostPagerModel(PostPagerDto pager, MainDto main) : base(main)
  {
    Pager = pager;
  }
  public PostPagerDto Pager { get; }
}
