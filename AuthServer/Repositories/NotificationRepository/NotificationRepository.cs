using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Repositories.NotificationRepository;

public interface INotificationRepository : IDeletableRepo<Notification>, IWritableRepo;

public class NotificationRepository(AuthDataContext authDataContext)
    : BaseRepository<Notification>(authDataContext),
        INotificationRepository
{
}
