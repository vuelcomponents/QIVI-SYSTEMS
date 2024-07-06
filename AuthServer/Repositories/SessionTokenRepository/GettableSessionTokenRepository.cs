using System.Linq.Expressions;
using authServer.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories.SessionTokenRepository;

public interface IGettableSessionTokenRepository : IWritableRepo, IGettableRepo<SessionToken>
{
    public SessionToken? Get(Expression<Func<SessionToken, bool>> predicate);
    public List<SessionToken> GetAll(Expression<Func<SessionToken, object>> predicate);
}

public class GettableSessionTokenRepository(AuthDataContext authDataContext)
    : BaseRepository<SessionToken>(authDataContext),
        IGettableSessionTokenRepository
{
    public SessionToken? Get(Expression<Func<SessionToken, bool>> predicate)
    {
        return AuthDataContext.SessionTokens.FirstOrDefault(predicate);
    }

    public SessionToken? GetById(long id, Expression<Func<SessionToken, object>> include)
    {
        return AuthDataContext.SessionTokens.Include(include).FirstOrDefault(u => u.Id == id);
    }

    public SessionToken? GetById(long id)
    {
        return AuthDataContext.SessionTokens.FirstOrDefault(u => u.Id == id);
    }

    public async Task<SessionToken?> GetByIdAsync(
        long id,
        Expression<Func<SessionToken, object>> include
    )
    {
        return await AuthDataContext
            .SessionTokens.Include(include)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<SessionToken?> GetByIdAsync(long id)
    {
        return await AuthDataContext.SessionTokens.FirstOrDefaultAsync(u => u.Id == id);
    }

    public List<SessionToken> GetAll()
    {
        return AuthDataContext.SessionTokens.ToList();
    }

    public List<SessionToken> GetAll(Expression<Func<SessionToken, object>> predicate)
    {
        return AuthDataContext.SessionTokens.Include(predicate).ToList();
    }
}
