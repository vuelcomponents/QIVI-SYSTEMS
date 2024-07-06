using authServer.Data;
using authServer.Exceptions;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Models;

namespace authServer.Services.DeviceService;

public interface IDeviceBlockService
{
    Task<List<DeviceDto>> DeviceBlock(
        User user,
        List<IdDto> devices,
        Device currentDevice,
        Int64? ownedUserId
    );
}

public class DeviceBlockService(AuthDataContext authDataContext, IMapper mapper)
    : IDeviceBlockService
{
    public async Task<List<DeviceDto>> DeviceBlock(
        User user,
        List<IdDto> devices,
        Device currentDevice,
        Int64? ownedUserId
    )
    {
        if (ownedUserId != null)
        {
            User? ownedUser = user.Users.FirstOrDefault(u => u.Id == ownedUserId);
            user = ownedUser ?? throw new Exception("userDoesNotBelongToTheAccount");
        }
        var devicesUpdated = new List<Device>();
        foreach (var idDto in devices)
        {
            var deviceId = idDto.Id;
            if (deviceId == currentDevice.Id)
            {
                throw new ActiveDeviceBlockException("youCannotBlockOngoingDevice");
            }
            var device = user.Devices.FirstOrDefault(d => d.Id == deviceId);
            if (device == null)
            {
                throw new ForeignDeviceException("deviceDoesNotBelongToTheAccount");
            }
            if (device.Blocked)
            {
                throw new DeviceBlockedException("deviceAlreadyBlocked");
            }
            device.Blocked = true;
            if (device.Token != null)
            {
                authDataContext.SessionTokens.Remove(device.Token);
            }
            devicesUpdated.Add(device);
        }
        await authDataContext.SaveChangesAsync();
        return devicesUpdated.Select(mapper.Map<DeviceDto>).ToList();
    }
}
