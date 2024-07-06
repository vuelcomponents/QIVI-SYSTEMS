namespace ClassLibrary.Dtos.Auth
{
    public class AuthResponse
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
    }
}
