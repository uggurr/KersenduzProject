using kersenduz.DataAccess.Context;
using kersenduz.DataAccess.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.DataAccess.UnitOfWork.Implement;

public class UnitOWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    public UnitOWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> SaveChangeAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}
