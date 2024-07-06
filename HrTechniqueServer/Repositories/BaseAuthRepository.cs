using HrTechniqueServer.Services;

namespace HrTechniqueServer.Repositories;

public interface IGettableAuthRepo<T>
{
    T? GetById(long id);
    Task<T?> GetByIdAsync(long id);
}

public abstract class BaseAuthRepository<T>(IAuthServiceConnector authServiceConnector)
    where T : class
{
    protected readonly IAuthServiceConnector AuthServiceConnector = authServiceConnector;
}
