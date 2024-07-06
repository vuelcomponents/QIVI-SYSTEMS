using ClassLibrary.Dtos.SharedDtos;

namespace HrTechniqueServer.Models;

public class File : IdCodeDescriptionDto
{
    public required byte[] FileData { get; set; }
    public required string FileName { get; set; }
}
