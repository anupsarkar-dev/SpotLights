namespace SpotLights.Shared;

public class PostViewModel : MainViewModel
{
  public PostViewModel(PostSlugDto postSlug, string categoriesUrl, MainDto main) : base(main)
  {
    PostSlug = postSlug;
    CategoriesUrl = categoriesUrl;
  }
  public PostSlugDto PostSlug { get; set; }
  public string CategoriesUrl { get; set; }
}
