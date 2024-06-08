namespace SpotLights.Shared;

public class CategoryViewModel : PostPagerModel
{
  public string Category { get; set; }

  public CategoryViewModel(string category, PostPagerDto pager, MainDto main) : base(pager, main)
  {
    Category = category;
  }
}
