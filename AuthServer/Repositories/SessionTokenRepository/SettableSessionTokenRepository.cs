using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Repositories.SessionTokenRepository;

public interface ISettableSessionTokenRepository : IWritableRepo, IDeletableRepo<SessionToken>;

public class SettableSessionTokenRepository(AuthDataContext authDataContext)
    : BaseRepository<SessionToken>(authDataContext),
        ISettableSessionTokenRepository
{
    public List<SessionToken> DeleteMany(List<SessionToken> entities)
    {
        AuthDataContext.SessionTokens.RemoveRange(entities);
        return entities;
    }
}
