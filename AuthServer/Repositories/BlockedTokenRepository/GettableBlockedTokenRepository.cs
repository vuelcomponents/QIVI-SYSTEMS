using System.Linq.Expressions;
using authServer.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories.BlockedTokenRepository;

public interface IGettableBlockedTokenRepository : IQuestionableRepo<BlockedToken>
{
    BlockedToken? GetByToken(string token);
    Task<BlockedToken?> GetByTokenAsync(string token);
    List<BlockedToken> GetAll(Expression<Func<BlockedToken, bool>> predicate);
}

public class GettableBlockedTokenRepository(AuthDataContext authDataContext)
    : BaseRepository<BlockedToken>(authDataContext),
        IGettableBlockedTokenRepository
{
    public async Task<BlockedToken?> GetByTokenAsync(string token)
    {
        return await AuthDataContext.BlockedTokens.FirstOrDefaultAsync(t => t.Token == token);
    }

    public BlockedToken? GetByToken(string token)
    {
        return AuthDataContext.BlockedTokens.FirstOrDefault(t => t.Token == token);
    }

    public List<BlockedToken> GetAll(Expression<Func<BlockedToken, bool>> predicate)
    {
        return AuthDataContext.BlockedTokens.Where(predicate).ToList();
    }

    public bool Has(Expression<Func<BlockedToken, bool>> predicate)
    {
        return AuthDataContext.BlockedTokens.Any(predicate);
    }
}
