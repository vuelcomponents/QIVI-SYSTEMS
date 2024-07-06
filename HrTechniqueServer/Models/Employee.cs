using System.ComponentModel.DataAnnotations;
using ClassLibrary.Dtos.SharedDtos;

namespace HrTechniqueServer.Models;

public class Employee : PersonalDataDto
{
    [Key]
    public long Id { get; set; }
    public long UserId { get; set; }
}
