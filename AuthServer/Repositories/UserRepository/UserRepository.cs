using System.Linq.Expressions;
using authServer.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories.UserRepository;

public interface IUserRepository
    : IWritableRepo,
        IGettableRepo<User>,
        IDeletableRepo<User>,
        IAddableRepo<User>
{
    public Task<User?> GetAndIncludeDeviceWithTokenAsync(Expression<Func<User, bool>> predicate);
    public User? GetAndIncludeAll(Expression<Func<User, bool>> predicate);
    public List<User> GetAdminUsers(User user);
}

public class UserRepository(AuthDataContext authDataContext)
    : BaseRepository<User>(authDataContext),
        IUserRepository
{
    public User? GetAndIncludeAll(Expression<Func<User, bool>> predicate)
    {
        return AuthDataContext
            .Users.Include(u => u.Devices)
            .ThenInclude(d => d.Token)
            .Include(u => u.Users)
            .ThenInclude(u => u.Devices)
            .ThenInclude(d => d.Token)
            .Include(u => u.Notifications)
            .Include(u => u.Licences)
            .FirstOrDefault(predicate);
    }

    public async Task<User?> GetAndIncludeDeviceWithTokenAsync(
        Expression<Func<User, bool>> predicate
    )
    {
        return await AuthDataContext
            .Users.Include(u => u.Devices)
            .ThenInclude(d => d.Token)
            .FirstOrDefaultAsync(predicate);
    }

    public List<User> GetAdminUsers(User user)
    {
        var userIds = user.Users.Select(au => au.Id).ToList();
        return AuthDataContext
            .Users.Include(u => u.Devices)
            .ThenInclude(d => d.Token)
            .Include(u => u.Licences)
            .Where(u => userIds.Contains(u.Id))
            .ToList();
    }

    
}
