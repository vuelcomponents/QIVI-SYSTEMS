using ClassLibrary.Dtos.SharedDtos;

namespace ClassLibrary.Dtos.Auth;

public class NotificationRequest
{
    public required List<IdDto> Users { get; set; }
    public required NotificationDto Notification { get; set; }
}