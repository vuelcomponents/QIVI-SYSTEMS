using authServer.Options;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Models;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Services.UserService;

public interface IUserDeleteService
{
    Task<List<UserDto>> DeleteMany(User admin, List<IdDto> users);
}

public class UserDeleteService(
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
        IUserDeleteService
{
    public async Task<List<UserDto>> DeleteMany(User admin, List<IdDto> users)
    {
        List<User> dbUsers = admin
            .Users.ToList()
            .Where(dbU => users.Any(u => u.Id == dbU.Id))
            .ToList();
        UserRepository.DeleteMany(dbUsers);
        await UserRepository.SaveAsync();
        return dbUsers.Select(Mapper.Map<UserDto>).ToList();
    }
}
