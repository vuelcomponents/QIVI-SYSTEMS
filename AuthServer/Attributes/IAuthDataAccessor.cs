using ClassLibrary.Models;

namespace authServer.Attributes;

public interface IAuthDataAccessor
{
    User? User { get; set; }
    Device? Device { get; set; }
}
