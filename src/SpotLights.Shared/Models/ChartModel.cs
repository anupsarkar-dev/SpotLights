using System.Collections.Generic;

namespace SpotLights.Shared;

public class BarChartModel
{
  public ICollection<string> Labels { get; set; } = default!;
  public ICollection<int> Data { get; set; } = default!;
}
