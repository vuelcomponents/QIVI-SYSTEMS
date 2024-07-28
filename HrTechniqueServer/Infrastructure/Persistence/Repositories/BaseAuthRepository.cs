using HrTechniqueServer.Infrastructure.Clients;

namespace HrTechniqueServer.Infrastructure.Persistence.Repositories;

public interface IGettableAuthRepo<T>
{
    T? GetById(long id);
    Task<T?> GetByIdAsync(long id);
}

public abstract class BaseAuthRepository<T>(AuthClient authClient)
    where T : class
{
    protected readonly AuthClient AuthClient = authClient;
}
