using System.Linq.Expressions;
using ClassLibrary.Dtos.SharedDtos;
using HrTechniqueServer.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace HrTechniqueServer.Infrastructure.Persistence.Repositories;

public interface IGettableRepo<T>
{
    T? Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null);
    Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>? include = null
    );
    List<T> GetAll(Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? include);
    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate,
        Expression<Func<T, object>>? include
    );
    List<T> GetAll(Expression<Func<T, bool>>? predicate = null);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
}

public interface IAddableRepo<T>
{
    T Create(T entity);
}

public interface IDeletableRepo
{
    Task DeleteManyByIdDtoList(List<IdDto> entities);
}

public interface IWritableRepo
{
    Task SaveAsync();
    void Save();
}

public interface IQuestionableRepo<T>
{
    bool Has(Expression<Func<T, bool>> predicate);
}

public abstract class BaseRepository<T>(HrtDataContext hrtDataContext)
    : IWritableRepo, 
      IGettableRepo<T>,
      IDeletableRepo 
    where T : class
{
    protected readonly HrtDataContext HrtDataContext = hrtDataContext;

    public virtual T? Get(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>? include = null
    )
    {
        return include != null
            ? HrtDataContext.Set<T>().Include(include).FirstOrDefault(predicate)
            : HrtDataContext.Set<T>().FirstOrDefault(predicate);
    }

    public virtual async Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>? include = null
    )
    {
        return include != null
            ? await HrtDataContext.Set<T>().Include(include).FirstOrDefaultAsync(predicate)
            : await HrtDataContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public virtual List<T> GetAll(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate != null
            ? HrtDataContext.Set<T>().Where(predicate).ToList()
            : HrtDataContext.Set<T>().ToList();
    }

    public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate != null
            ? await HrtDataContext.Set<T>().Where(predicate).ToListAsync()
            : await HrtDataContext.Set<T>().ToListAsync();
    }

    public virtual List<T> GetAll(
        Expression<Func<T, bool>>? predicate,
        Expression<Func<T, object>>? include
    )
    {
        IQueryable<T> query = HrtDataContext.Set<T>();

        if (include != null)
        {
            query = query.Include(include);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query.ToList();
    }

    public virtual async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate,
        Expression<Func<T, object>>? include
    )
    {
        IQueryable<T> query = HrtDataContext.Set<T>();

        if (include != null)
        {
            query = query.Include(include);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public virtual T Create(T entity)
    {
        HrtDataContext.Set<T>().Add(entity);
        return entity;
    }

    public virtual async Task DeleteManyByIdDtoList(List<IdDto> entities)
    {
        List<long> productIds = entities.Select(p => p.Id).ToList();
        await HrtDataContext.Employees.Where(dbP => productIds.Contains(dbP.Id)).DeleteAsync();
    }

    public virtual async Task SaveAsync()
    {
        await HrtDataContext.SaveChangesAsync();
    }

    public virtual void Save()
    {
        HrtDataContext.SaveChanges();
    }
}
