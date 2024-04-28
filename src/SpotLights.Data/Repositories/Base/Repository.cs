using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SpotLights.Data.Interfaces.Base;
using SpotLights.Shared.Helper;

namespace SpotLights.Data.Repositories.Base
{
  public class Repository<T> : IRepository<T> where T : class
  {
    public Task<ApplicationResult<T, ValidationResult>> AddAsync(T entity)
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationResult<IList<T1>, ValidationResult>> AddRangeAsync<T1>(IList<T1> entities) where T1 : class
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationResult<T, ValidationResult>> UpdateAsync(T entity)
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationResult<IList<T>, ValidationResult>> UpdateRangeAsync(IList<T> entity)
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationResult<T, ValidationResult>> DeleteAsync(T entity)
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationResult<IList<T>, ValidationResult>> DeleteRangeAsync(IList<T> entity)
    {
      throw new NotImplementedException();
    }

    public Task<ICollection<T>> GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
      throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
      throw new NotImplementedException();
    }

    public IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> predicate)
    {
      throw new NotImplementedException();
    }

    public Task<T> GetFirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
    {
      throw new NotImplementedException();
    }

    public IQueryable<T> GetAsNoTrackingWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
      throw new NotImplementedException();
    }
  }
}
