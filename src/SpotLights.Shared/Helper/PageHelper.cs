namespace SpotLights.Shared;

public static class PageHelper
{
  public static string CheckGetAvatarUrl(string? avatar)
  {
    if (!string.IsNullOrEmpty(avatar)) return avatar;
    return SpotLightsSharedConstant.DefaultAvatar;
  }

  public static string CheckGetCoverrUrl(string? avatar)
  {
    if (!string.IsNullOrEmpty(avatar)) return avatar;
    return SpotLightsSharedConstant.DefaultCover;
  }
}
