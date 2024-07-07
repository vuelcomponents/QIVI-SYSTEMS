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

public interface IUserNotifyService
{
    Task<Notification> Notify(List<IdDto> users, NotificationDto notification);
}

public class UserNotifyService(
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
        IUserNotifyService
{
    public async Task<Notification> Notify(List<IdDto> users, NotificationDto notification)
    {
        if (users.Count < 1)
            throw new Exception("noReceiver");
        foreach (var u in users)
        {
            var dbUser =
                await UserRepository.GetAsync((dbU=>dbU.Id.Equals(u.Id)),(dbU => dbU.Notifications))
                ?? throw new UserDoesNotExistException();
            if (dbUser.Notifications.Count > 10)
            {
                dbUser.Notifications.Remove(dbUser.Notifications.MinBy(n => n.TimeStamp)!);
            }
            dbUser.Notifications.Add(Mapper.Map<Notification>(notification));
            await UserRepository.SaveAsync();
        }
        return notification;
    }
}
