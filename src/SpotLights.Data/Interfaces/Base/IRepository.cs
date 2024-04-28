using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SpotLights.Shared.Helper;

namespace SpotLights.Data.Interfaces.Base
{
  public interface IRepository<T> where T : class
  {
    Task<ApplicationResult<T, ValidationResult>> AddAsync(T entity);
    Task<ApplicationResult<IList<T>, ValidationResult>> AddRangeAsync<T>(IList<T> entities) where T : class;
    Task<ApplicationResult<T, ValidationResult>> UpdateAsync(T entity);
    Task<ApplicationResult<IList<T>, ValidationResult>> UpdateRangeAsync(IList<T> entity);
    Task<ApplicationResult<T, ValidationResult>> DeleteAsync(T entity);
    Task<ApplicationResult<IList<T>, ValidationResult>> DeleteRangeAsync(IList<T> entity);


    Task<ICollection<T>> GetAllAsync();
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(int id);
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> predicate);
    Task<T> GetFirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAsNoTrackingWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
  }
}
