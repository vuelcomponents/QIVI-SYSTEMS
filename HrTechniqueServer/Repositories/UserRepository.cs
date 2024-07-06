using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using HrTechniqueServer.Exceptions;
using HrTechniqueServer.Services;

namespace HrTechniqueServer.Repositories;

public interface IUserRepository
{
    Task<UserShortDto> GetUserById(long? id);
}

public class UserRepository(IAuthServiceConnector authServiceConnector)
    : BaseAuthRepository<User>(authServiceConnector),
        IUserRepository
{
    public async Task<UserShortDto> GetUserById(long? id)
    {
        if (id == null)
        {
            throw new IdNotProvidedException();
        }
        return await AuthServiceConnector.Get<UserShortDto>($"integration/get-user-by-id/{id}");
    }
}
