using ClassLibrary.Dtos.Auth;

namespace HrTechniqueServer.Shared.Resources;

public interface IAuthResource
{
    UserShortDto? User { get; set; }
    AuthResponseShort? AuthResponse { get; set; }
}

public class AuthResource : IAuthResource
{
    public UserShortDto? User { get; set; }
    public AuthResponseShort? AuthResponse { get; set; }
}