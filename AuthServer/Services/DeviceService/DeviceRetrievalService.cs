using authServer.Exceptions;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;

namespace authServer.Services.DeviceService;

public interface IDeviceRetrievalService
{
    public List<DeviceDto> GetDevices(User user, Int64? ownedUserId);
}

public class DeviceRetrievalService(IMapper mapper) : IDeviceRetrievalService
{
    public List<DeviceDto> GetDevices(User user, Int64? ownedUserId)
    {
        if (ownedUserId != null)
        {
            User? ownedUser = user.Users.FirstOrDefault(u => u.Id == ownedUserId);
            user =
                ownedUser ?? throw new ForeignUserException("User does not belong to the account");
        }
        return user.Devices.Select(mapper.Map<DeviceDto>).ToList();
    }
}
