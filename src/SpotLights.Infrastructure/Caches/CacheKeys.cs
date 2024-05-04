namespace SpotLights.Infrastructure.Caches;

public static class CacheKeys
{
  public const string BlogData = "SpotLights";
  public const string BlogMailData = $"{BlogData}:mail";
  public const string CategoryItemes = $"{BlogData}:category:itemes";
}
