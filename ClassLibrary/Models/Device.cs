using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models;

public class Device
{
    [Key]
    public long Id { get; set; }
    public long UserId { get; set; }
    public required string Ip { get; set; }
    public required string UserAgent { get; set; }
    public bool Verified { get; set; } = false;
    public SessionToken? Token { get; set; }
    public long? TokenId { get; set; }
    public bool Blocked { get; set; } = false;
    public bool IpBlocked { get; set; } = false;
    public DateTime? SignedInDateTime { get; set; }
}