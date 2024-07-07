using System.Linq.Expressions;
using authServer.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories.BlockedTokenRepository;

public interface IBlockedTokenRepository
    : IWritableRepo,
        IGettableRepo<BlockedToken>,
        IDeletableRepo<BlockedToken>,
        IQuestionableRepo<BlockedToken>
{
    BlockedToken Create(BlockedToken entity, string discriminator);
}

public class BlockedTokenRepository(AuthDataContext authDataContext)
    : BaseRepository<BlockedToken>(authDataContext),
        IBlockedTokenRepository
{
    public bool Has(Expression<Func<BlockedToken, bool>> predicate)
    {
        return AuthDataContext.BlockedTokens.Any(predicate);
    }

    public BlockedToken Create(BlockedToken entity, string discriminator)
    {
        entity.Discriminator = discriminator;
        AuthDataContext.BlockedTokens.Add(entity);
        return entity;
    }
    
}
