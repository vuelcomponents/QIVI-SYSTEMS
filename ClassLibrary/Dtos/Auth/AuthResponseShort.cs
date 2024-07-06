namespace ClassLibrary.Dtos.Auth;

public class AuthResponseShort
{
    public UserShortDto? User { get; set; }
    public string? Token { get; set; }
}