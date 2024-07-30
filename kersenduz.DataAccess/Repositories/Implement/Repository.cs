using kersenduz.DataAccess.Context;
using kersenduz.DataAccess.Repositories.Interface;
using kersenduz.Shared.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.DataAccess.Repositories.Implement;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    private readonly AppDbContext _appDbContext;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public DbSet<T> Entity => _appDbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        var result = await Entity.AddAsync(entity);
        return result.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await Entity.AddRangeAsync(entities);
    }

    public T Delete(T entity)
    {
        var result = Entity.Remove(entity);
        return result.Entity;
    }

    public T DeleteByIdAsync(int id)
    {
        var entity = Entity.FirstOrDefault(x => x.Id == id);
        return Delete(entity);
    }

    public void DeleteRangeAsync(IEnumerable<T> entities)
    {
        Entity.RemoveRange(entities);
    }

    public IQueryable<T> GetAll()
    {
        return Entity.AsQueryable();
    }

    public async Task<T> GetByIdAsyn(int id)
    {
        return await Entity.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
    {
        return Entity.Where(expression);
    }

    public async Task<List<T>> ListAsync()
    {
        return await Entity.ToListAsync();
    }

    public async Task<List<T>> ListAync(Expression<Func<T, bool>> expression)
    {
        return await Entity.Where(expression).ToListAsync();
    }

    public T UpdateAsync(T entity)
    {
        var result = Entity.Update(entity);
        return result.Entity;
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        Entity.UpdateRange(entities);
    }
}
