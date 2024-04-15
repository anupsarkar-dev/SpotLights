namespace SpotLights.Shared;

public class MainModel
{
  public MainDto Main { get; set; }

  public MainModel(MainDto main)
  {
    Main = main;
  }
}
