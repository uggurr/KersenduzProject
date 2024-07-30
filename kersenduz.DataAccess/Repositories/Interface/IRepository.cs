using kersenduz.Shared.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.DataAccess.Repositories.Interface;

public interface IRepository<T> where T : BaseModel
{
    DbSet<T> Entity { get; }
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    T UpdateAsync(T entity);
    void UpdateRange(IEnumerable<T> entities);
    T Delete(T entity);
    void DeleteRangeAsync(IEnumerable<T> entities);
    T DeleteByIdAsync(int id);
    Task<List<T>> ListAsync();
    Task<List<T>> ListAync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsyn(int id);
    IQueryable<T> GetAll();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);


}
