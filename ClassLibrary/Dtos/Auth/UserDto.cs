
using System.ComponentModel.DataAnnotations;
using ClassLibrary.Enums;
using ClassLibrary.Models;

namespace ClassLibrary.Dtos.Auth
{
    public class UserDto
    {
        public long? Id { get; set; }
        [MaxLength(200)]
        [MinLength(5)]
        [EmailAddress]
        public required string Email { get; set; }
        public string? Password { get; set; }
        public bool? HrtLicence { get; set; }
        public bool? LombanditLicence { get; set; }
        public DateTime? SignedInDateTime { get; set; }
        public Role? Role { get; set; }
        public DateTime? ResetPassword { get; set; }
        public bool? Verified { get; set; }

        //Personal 
        [MaxLength(50)] public string FirstName { get; set; } = "";
        [MaxLength(50)] public string LastName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;
        [MaxLength(50)] public string PersonalId { get; set; } = "";
        public Gender? Gender { get; set; }
        [MaxLength(16)] public string PersonalPhone { get; set; } = "";
        [MaxLength(100)] public string Address { get; set; } = "";
        [MaxLength(50)] public string PostCode { get; set; } = "";
        [MaxLength(50)] public string City { get; set; } = "";

        //Business
        [MaxLength(50)] public string CompanyNip { get; set; } = "";
        [MaxLength(50)] public string CompanyName { get; set; } = "";
        [MaxLength(50)] public string CompanyAddress { get; set; } = "";
        [MaxLength(15)] public string CompanyPhone { get; set; } = "";
        [MaxLength(100)] public string Website { get; set; } = "";
        public List<NotificationDto> Notifications { get; set; } = [];
        public bool? Notified { get; set; }

        public List<User>? Users { get; set; }
        public long? UserId { get; set; }
        public bool? ClaimDeviceVerification { get; set; }
        public bool? LockedChoiceClaimDeviceVerification { get; set; }
        public bool? SuppressSelfSecurityChanges { get; set; }
        //Licences
        public List<LicenceDto> Licences { get; set; } = [];

        [Range(1, 120, ErrorMessage = "claimTokenExpiryMinutesMustBeBetween1and120.")]
        public int? ClaimTokenExpiryMinutes { get; set; } = Globals.TokenExpiryMinutes;
        public bool? LockedClaimTokenExpiryMinutes { get; set; }
        public bool? SuppressTokenRefresh { get; set; }
        public int? MaxUsersCount { get; set; }
        public bool Blocked { get; set; } = false;
    }
}
