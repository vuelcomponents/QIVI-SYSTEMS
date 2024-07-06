using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dtos.SharedDtos;

public class IdCodeDescriptionDto : IdCodeDto
{
    [MaxLength(500)]
    public string Description { get; set; } = "";
}