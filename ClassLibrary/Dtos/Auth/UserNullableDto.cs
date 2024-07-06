
using System.ComponentModel.DataAnnotations;
using ClassLibrary.Enums;
using ClassLibrary.Models;

namespace ClassLibrary.Dtos.Auth
{
    public class UserNullableDto
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? HrtLicence { get; set; }
        public bool? LombanditLicence { get; set; }
        public DateTime? SignedInDateTime { get; set; }
        public Role? Role { get; set; }
        public DateTime? ResetPassword { get; set; }
        public bool? Verified { get; set; }

        //Personal 
        public string? FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;
        public string? PersonalId { get; set; } = "";
        public Gender? Gender { get; set; }
        public string? PersonalPhone { get; set; } = "";
        public string? Address { get; set; } = "";
        public string? PostCode { get; set; } = "";
        public string? City { get; set; } = "";

        public string? CompanyNip { get; set; } = "";
        public string? CompanyName { get; set; } = "";
        public string? CompanyAddress { get; set; } = "";
        public string? CompanyPhone { get; set; } = "";
        public string? Website { get; set; } = "";
        public List<NotificationDto>? Notifications { get; set; } = [];


        public List<User>? Users { get; set; }
        public long? UserId { get; set; }
        public bool? ClaimDeviceVerification { get; set; }
        public bool? LockedChoiceClaimDeviceVerification { get; set; }
        public bool? SuppressSelfSecurityChanges { get; set; }
        //Licences
        public List<LicenceDto>? Licences { get; set; } = [];

        public int? ClaimTokenExpiryMinutes { get; set; } = Globals.TokenExpiryMinutes;
        public bool? LockedClaimTokenExpiryMinutes { get; set; }
        public bool? SuppressTokenRefresh { get; set; }
        public int? MaxUsersCount { get; set; }
        public bool? Blocked { get; set; } = false;
    }
}
