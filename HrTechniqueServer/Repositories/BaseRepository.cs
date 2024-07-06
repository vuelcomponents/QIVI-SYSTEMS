using System.Linq.Expressions;
using ClassLibrary.Dtos.SharedDtos;
using HrTechniqueServer.Data;

namespace HrTechniqueServer.Repositories;

public interface IGettableRepo<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(long id);
}

public interface ISettableRepo<T>
{
    T Create(T entity);
    T Update(T entity);
}

public interface IDeletableRepo<T>
{
    List<T> DeleteMany(List<IdDto> entities);
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

public abstract class BaseRepository<T>(HrtDataContext hrtDataContext) : IWritableRepo
    where T : class
{
    protected readonly HrtDataContext HrtDataContext = hrtDataContext;

    public async Task SaveAsync()
    {
        await HrtDataContext.SaveChangesAsync();
    }

    public void Save()
    {
        HrtDataContext.SaveChanges();
    }
}
