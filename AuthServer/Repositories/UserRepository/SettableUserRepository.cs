using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Repositories.UserRepository;

public interface ISettableUserRepository : IWritableRepo, ISettableRepo<User>, IDeletableRepo<User>;

public class SettableUserRepository(AuthDataContext authDataContext)
    : BaseRepository<User>(authDataContext),
        ISettableUserRepository
{
    public User Create(User entity)
    {
        AuthDataContext.Users.Add(entity);
        return entity;
    }

    public User Update(User entity)
    {
        AuthDataContext.Users.Update(entity);
        return entity;
    }

    public List<User> DeleteMany(List<User> entities)
    {
        AuthDataContext.Users.RemoveRange(entities);
        return entities;
    }
}
