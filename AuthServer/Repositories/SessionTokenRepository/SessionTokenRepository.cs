using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Repositories.SessionTokenRepository;

public interface ISessionTokenRepository
    : IWritableRepo,
        IGettableRepo<SessionToken>,
        IDeletableRepo<SessionToken>;

public class SessionTokenRepository(AuthDataContext authDataContext)
    : BaseRepository<SessionToken>(authDataContext),
        ISessionTokenRepository
{
    
}
