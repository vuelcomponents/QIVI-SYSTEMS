using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dtos.Auth;

public class DeviceDto
{
    public long Id { get; set; }
    public required string Ip { get; set; }
    public required string UserAgent { get; set; }
    public bool Active { get; set; }
    public bool Blocked { get; set; }
    public bool IpBlocked { get; set; }
    public bool Verified { get; set; } = false;
    public DateTime? SignedInDateTime { get; set; }
}