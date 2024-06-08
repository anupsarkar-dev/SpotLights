using System.Collections.Generic;

namespace SpotLights.Shared;

public class BarChartViewModel
{
  public ICollection<string> Labels { get; set; } = default!;
  public ICollection<int> Data { get; set; } = default!;
}
