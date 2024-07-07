using System.Linq.Expressions;
using authServer.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Repositories.LicenceRepository;

public interface ILicenceRepository : IGettableRepo<Licence>, IWritableRepo;

public class LicenceRepository(AuthDataContext authDataContext)
    : BaseRepository<Licence>(authDataContext),
        ILicenceRepository { }
