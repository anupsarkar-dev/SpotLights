namespace SpotLights.Infrastructure.Caches;

internal static class CacheKeys
{
    public const string BlogData = "SpotLights";
    public const string BlogMailData = $"{BlogData}:mail";
    public const string CategoryItemes = $"{BlogData}:category:itemes";
}
