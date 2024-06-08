namespace SpotLights.Shared;

public class MainViewModel
{
  public MainDto Main { get; set; }

  public MainViewModel(MainDto main)
  {
    Main = main;
  }
}
