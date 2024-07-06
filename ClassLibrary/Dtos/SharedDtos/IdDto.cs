using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dtos.SharedDtos;

public class IdDto
{
    [Key]
    public long Id { get; set; }
}