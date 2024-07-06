using System.ComponentModel.DataAnnotations;
using ClassLibrary.Enums;
using ClassLibrary.Models;

namespace ClassLibrary.Dtos.Auth;

public class UserUpdateDto
{
    public long? Id { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public bool? HrtLicence { get; set; }
    public bool? LombanditLicence { get; set; }

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


    //Business [MaxLength(50)] public string CompanyNip { get; set; } = "";
    [MaxLength(50)] public string CompanyName { get; set; } = "";
    [MaxLength(50)] public string CompanyAddress { get; set; } = "";
    [MaxLength(15)] public string CompanyPhone { get; set; } = "";
    [MaxLength(100)] public string Website { get; set; } = "";

    //Licences
    public List<Licence> Licences { get; set; } = [];

    public bool? ClaimDeviceVerification { get; set; }
    public bool? LockedChoiceClaimDeviceVerification { get; set; }
    [Range(1, 120, ErrorMessage = "claimTokenExpiryMinutesMustBeBetween1and120.")]
    public int? ClaimTokenExpiryMinutes { get; set; } = Globals.TokenExpiryMinutes;
    public bool? LockedClaimTokenExpiryMinutes { get; set; }
    public bool? SuppressTokenRefresh { get; set; }
    public bool? Blocked { get; set; } = false;
    public bool? SuppressSelfSecurityChanges { get; set; }
}