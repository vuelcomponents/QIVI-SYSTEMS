using authServer.Exceptions;
using authServer.Repositories.NotificationRepository;
using authServer.Repositories.UserRepository;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using ClassLibrary.Utils;

namespace authServer.Services.AuthService;

public interface IAuthUserService
{
    Task<UserDto> UpdateSelf(User dbUser, UserUpdateDto user);
    Task<UserDto> GetSelf(Int64 userId);
    List<NotificationDto> GetNotifications(User user);
    Task<List<NotificationDto>> SeenNotification(User user, Int64 notificationId);
}

public class AuthUserService(
    IGettableUserRepository gettableUserRepository,
    ISettableUserRepository settableUserRepository,
    ISettableNotificationRepository settableNotificationRepository,
    IMapper mapper,
    IQuickActions quickActions
) : IAuthUserService
{
    public async Task<UserDto> UpdateSelf(User dbUser, UserUpdateDto user)
    {
        List<string> excluded = ["Email", "Blocked", "Licences", "SuppressSelfSecurityChanges"];
        if (dbUser is { SuppressSelfSecurityChanges: true, Role: < Role.ADMIN })
        {
            excluded.AddRange(
                ["ClaimDeviceVerification", "ClaimTokenExpiryMinutes", "SuppressTokenRefresh"]
            );
        }
        quickActions.QuickUpdate(user, dbUser, excluded);
        await settableUserRepository.SaveAsync();
        return mapper.Map<UserDto>(dbUser);
    }

    public async Task<UserDto> GetSelf(Int64 userId)
    {
        return mapper.Map<UserDto>(await gettableUserRepository.GetByIdAsync(userId))!;
    }

    public List<NotificationDto> GetNotifications(User user)
    {
        return user
            .Notifications.Where(n => !n.Seen)
            .Select(mapper.Map<NotificationDto>)
            .OrderByDescending(n => n.TimeStamp)
            .ToList();
    }

    public async Task<List<NotificationDto>> SeenNotification(User user, Int64 notificationId)
    {
        Notification dbNotification =
            user.Notifications.FirstOrDefault(n => n.Id == notificationId)
            ?? throw new NotificationDoesNotExistException();
        settableNotificationRepository.DeleteMany([dbNotification]);
        await settableNotificationRepository.SaveAsync();

        return user
            .Notifications.Where(n => !n.Seen)
            .Select(mapper.Map<NotificationDto>)
            .OrderByDescending(n => n.TimeStamp)
            .ToList();
    }
}
