using ClassLibrary.Dtos.SharedDtos;

namespace HrTechniqueServer.Domain.Entities;

public class File : IdCodeDescriptionDto
{
    public required byte[] FileData { get; set; }
    public required string FileName { get; set; }
}
