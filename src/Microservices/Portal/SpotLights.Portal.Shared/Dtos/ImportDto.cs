using SpotLights.Shared.Dtos;
using System.Collections.Generic;

namespace SpotLights.Shared;

public class ImportDto
{
    public string BaseUrl { get; set; } = default!;
    public List<PostEditorDto> Posts { get; set; } = default!;
}
