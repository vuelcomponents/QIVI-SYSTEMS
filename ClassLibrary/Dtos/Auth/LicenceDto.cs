using System.ComponentModel.DataAnnotations;
using ClassLibrary.Enums;
using ClassLibrary.Models;

namespace ClassLibrary.Dtos.Auth;

public class LicenceDto
{
    [Key]
    public long Id { get; set; }
    public required string Code { get; set; }
    public string Icon { get; set; } = "mdi mdi-crosshairs-question";
    public required string Host { get; set; }
    public required Software Software { get; set; }
    public required DateTime StartTimeStamp { get; set; }
    public required DateTime EndTimeStamp { get; set; }
    public required short MaxUsersQty { get; set; }
}