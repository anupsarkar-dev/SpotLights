using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace SpotLights.Common.Library.Utils;

public class DateTimetValueGenerator : ValueGenerator<DateTime>
{
  public override bool GeneratesTemporaryValues => false;

  public override DateTime Next(EntityEntry entry)
  {
    return DateTime.UtcNow;
  }

  public override ValueTask<DateTime> NextAsync(
    EntityEntry entry,
    CancellationToken cancellationToken = default
  )
  {
    return ValueTask.FromResult(DateTime.UtcNow);
  }
}
