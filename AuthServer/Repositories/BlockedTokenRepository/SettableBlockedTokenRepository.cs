using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Repositories.BlockedTokenRepository;

public interface ISettableBlockedTokenRepository : IWritableRepo, IDeletableRepo<BlockedToken>
{
    BlockedToken Create(BlockedToken entity, string discriminator);
    // public bool Has(string token, string? discriminator = null);
}

public class SettableBlockedTokenRepository(AuthDataContext authDataContext)
    : BaseRepository<BlockedToken>(authDataContext),
        ISettableBlockedTokenRepository
{
    public bool Has(string token, string? discriminator = null)
    {
        return AuthDataContext.BlockedTokens.Any(t =>
            (discriminator == null || discriminator == t.Discriminator) && t.Token == token
        );
    }

    public BlockedToken Create(BlockedToken entity, string discriminator)
    {
        entity.Discriminator = discriminator;
        AuthDataContext.BlockedTokens.Add(entity);
        return entity;
    }

    public BlockedToken Update(BlockedToken entity)
    {
        AuthDataContext.BlockedTokens.Update(entity);
        return entity;
    }

    public List<BlockedToken> DeleteMany(List<BlockedToken> entities)
    {
        AuthDataContext.BlockedTokens.RemoveRange(entities);
        return entities;
    }
}
