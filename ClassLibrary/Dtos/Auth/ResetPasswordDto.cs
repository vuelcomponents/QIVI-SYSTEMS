using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dtos.Auth;

public class ResetPasswordDto
{
    public required string Token { get; set; }
    [MinLength(8)]
    [MaxLength(100)]
    public required string NewPassword { get; set; }

}