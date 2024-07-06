namespace ClassLibrary.Dtos.Auth;

public class SessionTokenDto
{
    public long Id { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}