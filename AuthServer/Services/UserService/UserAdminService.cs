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

public interface IUserAdminService
{
    List<UserDto> GetAdminUsers(User user);
}

public class UserAdminService(
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
        IUserAdminService
{
    public List<UserDto> GetAdminUsers(User user)
    {
        return UserRepository
            .GetAdminUsers(user)
            .Select(u => Mapper.Map<UserDto>(u))
            .ToList();
    }
}
