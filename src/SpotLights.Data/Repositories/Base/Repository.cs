using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotLights.Data.Interfaces.Base;

namespace SpotLights.Data.Repositories.Base
{
  public class Repository<T> : IRepository<T> where T : class
  {
  }
}
