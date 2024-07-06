namespace ClassLibrary.Dtos.Auth;

public class UserStatDto
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public int? DevicesCount { get; set; } // donut
    public int? BlockedDevicesCount { get; set; } // kropki
    public int? BlockedIpsCount { get; set; } //kropki
    public bool? Verified { get; set; } // lista niezweryfikowanych
    public int? ClaimTokenExpiryMinutes { get; set; } // slupki
    public bool? ClaimDeviceVerification { get; set; } // lista nie uzywjacych
    public bool? SuppressTokenRefresh { get; set; }
    public bool? Active { get; set; }
    public int? ActiveDeviceCount { get; set; }
    public int? MonthlyVisits { get; set; }
    public int? MonthlyActivities { get; set; }

}