using System.ComponentModel.DataAnnotations;
using ClassLibrary.Enums;

namespace ClassLibrary.Models;

public class Licence
{
    [Key]
    public long Id { get; set; }
    public required string Code { get; set; }
    public string Icon { get; set; } = "mdi mdi-crosshairs-question";
    public required Software Software { get; set; }
    public required string Host { get; set; }
    public required DateTime StartTimeStamp { get; set; }
    public required DateTime EndTimeStamp { get; set; }
    public required short MaxUsersQty { get; set; }
    public List<User> Users { get; set; } = [];
}