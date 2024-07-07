using authServer.Exceptions;
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

public interface IUserBlockService
{
    Task<List<UserDto>> ChangeBlockStatus(User admin, List<IdDto> users, bool blocked);
}

public class UserBlockService(
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
        IUserBlockService
{
    public async Task<List<UserDto>> ChangeBlockStatus(User admin, List<IdDto> users, bool blocked)
    {
        List<User> blockedList = [];
        foreach (var user in users)
        {
            User adminUser =
                admin.Users.FirstOrDefault(u => u.Id == user.Id)
                ?? throw new UserDoesNotExistException();
            adminUser.Blocked = blocked;
            await UserRepository.SaveAsync();
            blockedList.Add(adminUser);
        }
        return blockedList.Select(Mapper.Map<UserDto>).ToList();
    }
}
