using SpotLights.Shared.Dtos;

namespace SpotLights.Shared.Dtos.Admin;

public class FrontPostImportDto : PostEditorDto
{
  public bool Selected { get; set; }
  public bool? ImportComplete { get; set; }
}
