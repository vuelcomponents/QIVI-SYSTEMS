using authServer.Exceptions;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;

namespace authServer.Services.DeviceService;

public interface IDeviceSignoutService
{
    public Task<DeviceDto> DeviceSignout(User user, Int64 deviceId, Int64? ownedUserId);
}

public class DeviceSignoutService(ITokenBlockService tokenBlockService, IMapper mapper)
    : IDeviceSignoutService
{
    public async Task<DeviceDto> DeviceSignout(User user, Int64 deviceId, Int64? ownedUserId)
    {
        if (ownedUserId != null)
        {
            User? ownedUser = user.Users.FirstOrDefault(u => u.Id == ownedUserId);
            user = ownedUser ?? throw new Exception("userDoesNotBelongToTheAccount");
        }

        var device =
            user.Devices.FirstOrDefault(d => d.Id == deviceId)
            ?? throw new ForeignDeviceException("deviceDoesNotBelongToTheAccount");

        if (device.Token != null)
            await tokenBlockService.Block(device.Token.Token);

        return mapper.Map<DeviceDto>(device);
    }
}
