using SpotLights.Shared;
using System.Collections.Generic;

namespace SpotLights.Shared.Dtos.Admin;

public class FrontImportDto : ImportDto
{
  public new List<FrontPostImportDto> Posts { get; set; } = default!;
}
