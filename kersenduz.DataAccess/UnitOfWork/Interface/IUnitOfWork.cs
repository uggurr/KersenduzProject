using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.DataAccess.UnitOfWork.Interface;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync();
}
