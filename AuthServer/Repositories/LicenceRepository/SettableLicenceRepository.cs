using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Repositories.LicenceRepository;

public interface ISettableLicenceRepository
    : IWritableRepo,
        ISettableRepo<Licence>,
        IDeletableRepo<Licence>;

public class SettableLicenceRepository(AuthDataContext authDataContext)
    : BaseRepository<Licence>(authDataContext),
        ISettableLicenceRepository
{
    public Licence Update(Licence entity)
    {
        throw new NotImplementedException();
    }

    public List<Licence> DeleteMany(List<Licence> entities)
    {
        throw new NotImplementedException();
    }

    public Licence Create(Licence entity)
    {
        throw new NotImplementedException();
    }
}
