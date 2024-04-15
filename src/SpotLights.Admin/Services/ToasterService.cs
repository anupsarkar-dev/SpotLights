using SpotLights.Shared.Resources;
using Microsoft.Extensions.Localization;
using Sotsera.Blazor.Toaster;
using System.Net.Http;

namespace SpotLights.Admin.Services;

public class ToasterService
{
  private readonly IToaster _toaster;
  private readonly IStringLocalizer<Resource> _localizer;
  public ToasterService(IToaster toaster, IStringLocalizer<Resource> localizer)
  {
    _toaster = toaster;
    _localizer = localizer;
  }

  public bool CheckResponse(HttpResponseMessage response)
  {
    if (response.IsSuccessStatusCode)
    {
      _toaster.Success(_localizer["completed"]);
      return true;
    }
    else
    {
      _toaster.Error(_localizer["generic-error"]);
      return false;
    }
  }
}
