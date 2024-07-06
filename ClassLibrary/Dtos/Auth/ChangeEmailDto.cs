using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dtos.Auth;

public class ChangeEmailDto
{
    [MaxLength(500)] public required string Password { get; set; }
    [MaxLength(200)] public required string NewEmail { get; set; }
}