using System.ComponentModel.DataAnnotations;
using ClassLibrary.Enums;

namespace ClassLibrary.Models
{
    public class User
    {
        [Key] public long Id { get; set; }
        public bool? Verified { get; set; }
        public required string Email { get; set; }
        public required byte[] Password { get; set; }
        public required byte[] Salt { get; set; }
        public required bool HrtLicence { get; set; }
        public required bool LombanditLicence { get; set; }
        public required Role Role { get; set; }

        //Personal 
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;
        public string PersonalId { get; set; } = "";
        public Gender? Gender { get; set; }
        public string PersonalPhone { get; set; } = "";
        public string Address { get; set; } = "";
        public string PostCode { get; set; } = "";
        public string City { get; set; } = "";

        //Business
        public string CompanyNip { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string CompanyAddress { get; set; } = "";
        public string CompanyPhone { get; set; } = "";
        public string Website { get; set; } = "";
        public List<Device> Devices { get; set; } = [];
        public List<Notification> Notifications { get; set; } = [];

        public List<User> Users { get; set; } = [];
        public long? UserId { get; set; }

        //Options
        public DateTime? ResetPasswordDateTime { get; set; }
        public DateTime? VerifyEmailDateTime { get; set; }
        public DateTime? VerifyDeviceDateTime { get; set; }
        public DateTime? ChangeEmailDateTime { get; set; }
        public string? NewEmail { get; set; }
        public byte[]? RefreshToken { get; set; }
        public bool Blocked { get; set; } = false;
        public bool SuppressSelfSecurityChanges { get; set; } = true;

        //Licences
        public List<Licence> Licences { get; set; } = [];

        // new
        public bool ClaimDeviceVerification { get; set; } = false;
        public bool LockedChoiceClaimDeviceVerification { get; set; } = false;
        [Range(1, 120, ErrorMessage = $"claimTokenExpiryMinutesMustBeBetween1and120.")]
        public int ClaimTokenExpiryMinutes { get; set; } = Globals.TokenExpiryMinutes;
        public bool LockedClaimTokenExpiryMinutes { get; set; } = false;
        public bool SuppressTokenRefresh { get; set; } = false;
        public int MaxUsersCount { get; set; } = 20;
        public int MonthlyVisits { get; set; } = 0;
        public int MonthlyActivities { get; set; } = 0;
    }
}
