using authServer.Exceptions;
using authServer.Options;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Services.UserService;

public interface IUserUpdateService
{
    Task<UserDto> UpdateUser(User admin, UserUpdateDto updateUser);
}

public class UserUpdateService(
    IMapper mapper,
    IQuickActions quickActions,
    IMailerVerificationService mailerVerificationService,
    IGettableUserRepository gettableUserRepository,
    ISettableUserRepository settableUserRepository,
    ITokenWriteService tokenWriteService,
    IOptions<JwtOptions> jwtOptions
)
    : BaseUserService(
        mapper,
        quickActions,
        mailerVerificationService,
        gettableUserRepository,
        settableUserRepository,
        tokenWriteService,
        jwtOptions
    ),
        IUserUpdateService
{
    public async Task<UserDto> UpdateUser(User admin, UserUpdateDto updateUser)
    {
        var dbUpdateUser =
            await GettableUserRepository.GetByIdAsync(updateUser.Id ?? 0, (u => u.Licences))
            ?? throw new UserDoesNotExistException("updatedUserCouldNotBeFound");

        if (!(admin.Users.Any(u => u.Id == dbUpdateUser.Id)))
        {
            throw new UserDoesNotExistException("userDoesNotBelongToTheAccount");
        }
        QuickActions.QuickUpdate(updateUser, dbUpdateUser, ["Email", "Licences", "Block"]);
        HandleLicences();
        await GettableUserRepository.SaveAsync();

        return Mapper.Map<UserDto>(dbUpdateUser);

        void HandleLicences()
        {
            List<Licence> licences = [];
            foreach (var licence in updateUser.Licences)
            {
                var dbLicence =
                    admin.Licences.FirstOrDefault(l => l.Id == licence.Id)
                    ?? throw new LicenceNotOwnedException();
                licences.Add(dbLicence);
            }
            dbUpdateUser.Licences = licences;
        }
    }
}
