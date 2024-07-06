using ClassLibrary.Models;

namespace authServer.Attributes;

public sealed class AuthDataAccessor : IAuthDataAccessor
{
    public User? User { get; set; }
    public Device? Device { get; set; }
}
