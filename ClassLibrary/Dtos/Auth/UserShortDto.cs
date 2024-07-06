namespace ClassLibrary.Dtos.Auth;

public class UserShortDto
{
    public long Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; } = "";
    public string? LastName { get; set; } = "";
    public bool? Notified { get; set; }
    public List<LicenceDto>? Licences { get; set; } = [];
}