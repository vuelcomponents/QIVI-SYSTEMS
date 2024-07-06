using System.Linq.Expressions;
using authServer.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories.UserRepository;

public interface IGettableByEmailUserRepository
{
    public User? GetByEmail(string email, Expression<Func<User, object>> predicate);
    public User? GetByEmail(string email);
    public Task<User?> GetByEmailAsync(string email, Expression<Func<User, object>> predicate);
    public Task<User?> GetByEmailAsync(string email);
    public Task<User?> GetByEmailIncludeDeviceWithTokenAsync(string email);
}

public interface IGettableUserRepository
    : IWritableRepo,
        IGettableRepo<User>,
        IGettableByEmailUserRepository
{
    public User? GetByIdIncludeDeviceWithToken(long id);
    public User? GetByIdIncludeAll(long id);
    public List<User> GetAdminUsers(User user);
}

public sealed class GettableUserRepository(AuthDataContext authDataContext)
    : BaseRepository<User>(authDataContext),
        IGettableUserRepository
{
    public User? GetById(long id, Expression<Func<User, object>> include)
    {
        return AuthDataContext.Users.Include(include).FirstOrDefault(u => u.Id == id);
    }

    public User? GetById(long id)
    {
        return AuthDataContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public async Task<User?> GetByIdAsync(long id, Expression<Func<User, object>> include)
    {
        return await AuthDataContext.Users.Include(include).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await AuthDataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByIdAsync(long id, bool includeDeviceWithToken)
    {
        return await AuthDataContext
            .Users.Include(u => u.Devices)
            .ThenInclude(d => d.Token)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public User? GetByIdIncludeDeviceWithToken(long id)
    {
        return AuthDataContext
            .Users.Include(u => u.Devices)
            .ThenInclude(d => d.Token)
            .FirstOrDefault(u => u.Id == id);
    }

    public User? GetByIdIncludeAll(long id)
    {
        return AuthDataContext
            .Users.Include(u => u.Devices)
            .ThenInclude(d => d.Token)
            .Include(u => u.Users)
            .ThenInclude(u => u.Devices)
            .ThenInclude(d => d.Token)
            .Include(u => u.Notifications)
            .Include(u => u.Licences)
            .FirstOrDefault(u => u.Id == id);
    }

    public User? GetByEmail(string email, Expression<Func<User, object>> include)
    {
        return AuthDataContext.Users.Include(include).FirstOrDefault(u => u.Email == email);
    }

    public User? GetByEmail(string email)
    {
        return AuthDataContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public User? GetByEmail(string email, bool includeDeviceWithToken)
    {
        if (includeDeviceWithToken)
        {
            return AuthDataContext
                .Users.Include(u => u.Devices)
                .ThenInclude(d => d.Token)
                .FirstOrDefault(u => u.Email == email);
        }
        return null;
    }

    public async Task<User?> GetByEmailAsync(string email, Expression<Func<User, object>> include)
    {
        return await AuthDataContext
            .Users.Include(include)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await AuthDataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailIncludeDeviceWithTokenAsync(string email)
    {
        return await AuthDataContext
            .Users.Include(u => u.Devices)
            .ThenInclude(d => d.Token)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public List<User> GetAll()
    {
        return AuthDataContext.Users.ToList();
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
