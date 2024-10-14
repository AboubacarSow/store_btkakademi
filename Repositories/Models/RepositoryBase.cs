
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.Models;

public abstract class RepositoryBase<T> : IRepositoryBase<T>
where T : class, new()
{
    protected readonly RepositoryContext _context;

#pragma warning disable IDE0290 // Use primary constructor
    protected RepositoryBase(RepositoryContext context)
#pragma warning restore IDE0290 // Use primary constructor
    {
        _context = context;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges
               ?_context.Set<T>()
               :_context.Set<T>().AsNoTracking();
    }

    public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges
              ?_context.Set<T>().Where(expression).SingleOrDefault()
              :_context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
    }
    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
  
}