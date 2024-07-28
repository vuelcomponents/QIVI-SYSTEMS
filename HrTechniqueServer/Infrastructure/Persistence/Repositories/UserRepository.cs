using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using HrTechniqueServer.Domain.Exceptions;
using HrTechniqueServer.Infrastructure.Clients;

namespace HrTechniqueServer.Infrastructure.Persistence.Repositories;

public interface IUserRepository
{
    Task<UserShortDto> GetUserById(long? id);
}

public class UserRepository(AuthClient authClient)
    : BaseAuthRepository<User>(authClient),
        IUserRepository
{
    public async Task<UserShortDto> GetUserById(long? id)
    {
        if (id == null)
        {
            throw new IdNotProvidedException();
        }
        return await AuthClient.Get<UserShortDto>($"integration/get-user-by-id/{id}");
    }
}
