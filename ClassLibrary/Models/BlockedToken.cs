using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models;

public class BlockedToken
{
    [Key]
    public long Id { get; set; }
    public required string Token { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string? Discriminator { get; set; }
}