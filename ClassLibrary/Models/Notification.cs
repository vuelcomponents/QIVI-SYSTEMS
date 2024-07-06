using ClassLibrary.Dtos.SharedDtos;

namespace ClassLibrary.Models;

public class Notification : IdCodeDescriptionDto
{
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public bool Seen { get; set; } = false;
}