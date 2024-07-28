using System.ComponentModel.DataAnnotations;
using ClassLibrary.Dtos;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;

namespace HrTechniqueServer.Domain.Dto;

public class EmployeeDto : PersonalDataDto
{
    [Key]
    public long Id { get; set; }
    public UserShortDto? User { get; set; }
}
