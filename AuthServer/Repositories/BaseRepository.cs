using System.Linq.Expressions;
using authServer.Data;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories;

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

public interface IDeletableRepo<T>
{
    List<T> DeleteMany(List<T> entities);
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

public abstract class BaseRepository<T>(AuthDataContext authDataContext)
    : IWritableRepo,
        IGettableRepo<T>
    where T : class
{
    protected readonly AuthDataContext AuthDataContext = authDataContext;

    public virtual T? Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
    {
        return include != null
            ? AuthDataContext.Set<T>().Include(include).FirstOrDefault(predicate)
            : AuthDataContext.Set<T>().FirstOrDefault(predicate);
    }

    public virtual async Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>? include = null
    )
    {
        return include != null
            ? await AuthDataContext.Set<T>().Include(include).FirstOrDefaultAsync(predicate)
            : await AuthDataContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public virtual List<T> GetAll(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate != null
            ? AuthDataContext.Set<T>().Where(predicate).ToList()
            : AuthDataContext.Set<T>().ToList();
    }

    public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate != null
            ? await AuthDataContext.Set<T>().Where(predicate).ToListAsync()
            : await AuthDataContext.Set<T>().ToListAsync();
    }

    public virtual List<T> GetAll(
        Expression<Func<T, bool>>? predicate,
        Expression<Func<T, object>>? include
    )
    {
        IQueryable<T> query = AuthDataContext.Set<T>();

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
        IQueryable<T> query = AuthDataContext.Set<T>();

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
        AuthDataContext.Set<T>().Add(entity);
        return entity;
    }

    public virtual List<T> DeleteMany(List<T> entities)
    {
        AuthDataContext.Set<T>().RemoveRange(entities);
        return entities;
    }
    public virtual async Task SaveAsync()
    {
        await AuthDataContext.SaveChangesAsync();
    }

    public virtual void Save()
    {
        AuthDataContext.SaveChanges();
    }
}
