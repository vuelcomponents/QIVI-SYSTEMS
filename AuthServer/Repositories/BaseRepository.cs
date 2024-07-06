using System.Linq.Expressions;
using authServer.Data;

namespace authServer.Repositories;

public interface IGettableRepo<T>
{
    T? GetById(long id, Expression<Func<T, object>> include);
    T? GetById(long id);
    List<T> GetAll();
    Task<T?> GetByIdAsync(long id);
    Task<T?> GetByIdAsync(long id, Expression<Func<T, object>> include);
}

public interface ISettableRepo<T>
{
    T Create(T entity);
    T Update(T entity);
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

public abstract class BaseRepository<T>(AuthDataContext authDataContext) : IWritableRepo
    where T : class
{
    protected readonly AuthDataContext AuthDataContext = authDataContext;

    public virtual async Task SaveAsync()
    {
        await AuthDataContext.SaveChangesAsync();
    }

    public virtual void Save()
    {
        AuthDataContext.SaveChanges();
    }
}
