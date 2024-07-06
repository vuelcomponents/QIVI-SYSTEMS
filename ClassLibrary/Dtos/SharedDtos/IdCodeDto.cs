using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dtos.SharedDtos;

public class IdCodeDto : IdDto
{
    [MaxLength(100)]
    public required string Code { get; set; }
}