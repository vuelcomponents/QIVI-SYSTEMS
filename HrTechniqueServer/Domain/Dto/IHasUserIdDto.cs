using ClassLibrary.Dtos.Auth;

namespace HrTechniqueServer.Domain.Dto;

public interface IHasUserIdDto
{
    long UserId { get; set; }
    UserShortDto? User { get; set; }
}
