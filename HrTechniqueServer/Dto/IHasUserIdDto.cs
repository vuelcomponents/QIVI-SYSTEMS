using ClassLibrary.Dtos;
using ClassLibrary.Dtos.Auth;

namespace HrTechniqueServer.Dto;

public interface IHasUserIdDto
{
    long UserId { get; set; }
    UserShortDto? User { get; set; }
}
