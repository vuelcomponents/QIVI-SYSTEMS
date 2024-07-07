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
using Newtonsoft.Json;

namespace authServer.Services.UserService;

public interface IUserCreateService
{
    Task<UserDto> AddUser(User admin, UserDto createUser);
}

public class UserCreateService(
    IMapper mapper,
    IQuickActions quickActions,
    IMailerVerificationService mailerVerificationService,
    IUserRepository userRepository,
    ITokenWriteService tokenWriteService,
    IOptions<JwtOptions> jwtOptions
)
    : BaseUserService(
        mapper,
        quickActions,
        mailerVerificationService,
        userRepository,
        tokenWriteService,
        jwtOptions
    ),
        IUserCreateService
{
    public async Task<UserDto> AddUser(User admin, UserDto createUser)
    {
        if (admin.MaxUsersCount <= admin.Users.Count)
        {
            throw new ActivityDeclinedException("maximumNumberOfUsers");
        }
        if ((await UserRepository.GetAllAsync()).Any(u => u.Email == createUser.Email))
        {
            throw new ActivityDeclinedException("emailTaken");
        }

        createUser.Password = "configuration.Attributes";
        User dbUser = Mapper.Map<User>(createUser);
        dbUser.Verified = false;
        dbUser.ClaimTokenExpiryMinutes = JwtOptions.Value.TokenExpiryMinutes;
        HandleLicences();
        admin.Users.Add(dbUser);
        dbUser.VerifyEmailDateTime = DateTime.Now;
        await UserRepository.SaveAsync();

        _ = Task.Run(() =>
        {
            TokenWriteService.WriteVerificationToken(dbUser, out string vToken);
            MailerVerificationService.SendVerificationEmail(dbUser, vToken);
        });

        return Mapper.Map<UserDto>(dbUser);

        void HandleLicences()
        {
            foreach (var licence in createUser.Licences)
            {
                var dbLicence =
                    admin.Licences.FirstOrDefault(l => l.Id == licence.Id)
                    ?? throw new LicenceNotOwnedException();
                dbUser.Licences.Add(dbLicence);
            }
        }
    }
}
