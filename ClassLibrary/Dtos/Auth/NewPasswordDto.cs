using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dtos.Auth;

public class NewPasswordDto
{
    public required string OldPassword { get; set; }
    [MinLength(8)]
    [MaxLength(100)]
    public required string NewPassword { get; set; }
    public required string ReNewPassword { get; set; }

}