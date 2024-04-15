using SpotLights.Shared;

namespace SpotLights.Admin;

public class FrontPostImportDto : PostEditorDto
{
  public bool Selected { get; set; }
  public bool? ImportComplete { get; set; }
}
