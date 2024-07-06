using System.Linq.Expressions;
using authServer.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories.LicenceRepository;

public interface IGettableLicenceRepository : IWritableRepo, IGettableRepo<Licence>;

public class GettableLicenceRepository(AuthDataContext authDataContext)
    : BaseRepository<Licence>(authDataContext),
        IGettableLicenceRepository
{
    public Licence? GetById(long id, Expression<Func<Licence, object>> include)
    {
        return AuthDataContext.Licences.Include(include).FirstOrDefault(u => u.Id == id);
    }

    public Licence? GetById(long id)
    {
        return AuthDataContext.Licences.FirstOrDefault(u => u.Id == id);
    }

    public async Task<Licence?> GetByIdAsync(long id, Expression<Func<Licence, object>> include)
    {
        return await AuthDataContext.Licences.Include(include).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Licence?> GetByIdAsync(long id)
    {
        return await AuthDataContext.Licences.FirstOrDefaultAsync(u => u.Id == id);
    }

    public List<Licence> GetAll()
    {
        return AuthDataContext.Licences.ToList();
    }
}
