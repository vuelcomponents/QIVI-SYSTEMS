using ClassLibrary.Models;

namespace ClassLibrary.Dtos.SharedDtos;

public class PersonalDataDto
{

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime? DateOfBirth { get; set; } = DateTime.Now;
    public string PersonalId { get; set; } = "";
    public string PersonalPhone { get; set; } = "";
    public string Address { get; set; } = "";
    public string PostCode { get; set; } = "";
    public string City { get; set; } = "";
}