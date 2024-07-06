using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Repositories.NotificationRepository;

public interface ISettableNotificationRepository : IDeletableRepo<Notification>, IWritableRepo;

public class SettableNotificationRepository(AuthDataContext authDataContext)
    : BaseRepository<Notification>(authDataContext),
        ISettableNotificationRepository
{
    public List<Notification> DeleteMany(List<Notification> entities)
    {
        AuthDataContext.Notifications.RemoveRange(entities);
        return entities;
    }
}
