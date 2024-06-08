using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SpotLights.Shared.Helper;

namespace SpotLights.Shared.Extensions
{
  public static class CommonExtensions
  {
    /// <summary>
    /// Create API response of anonymous object with selective properties
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="originalObj"></param>
    /// <param name="properties"></param>
    /// <returns>Create API response of anonymous object with selective properties</returns>
    public static ApplicationSuccessResponse<dynamic> MapToResponse<TSource>(this ApplicationSuccessResponse<TSource> originalObj,
      params Expression<Func<TSource, object>>[] properties) where TSource : new()
    {
      dynamic response = new ExpandoObject();

      foreach (var property in properties)
      {
        string properName = originalObj.Data.GetMemberName(property);
        var propertyInfo = typeof(TSource).GetProperty(properName);
        if (propertyInfo != null)
        {
          ((IDictionary<string, object>)response)[properName] = propertyInfo.GetValue(originalObj.Data);
        }
      }

      return new ApplicationSuccessResponse<dynamic>()
      {
        Data = response,
        Message = originalObj.Message
      };
    }
  }
}
