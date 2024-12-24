using kersenduz.Core.Repository.Interface;
using kersenduz.DataAccess.Context;
using kersenduz.DataAccess.Repositories.Implement;
using kersenduz.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.Core.Repository.Implement
{
  internal class UnitRepository : Repository<Unit>, IUnitRepository
  {
    public UnitRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
  }
}
