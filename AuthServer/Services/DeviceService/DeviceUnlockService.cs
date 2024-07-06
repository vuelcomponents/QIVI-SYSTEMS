using authServer.Data;
using authServer.Exceptions;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Models;

namespace authServer.Services.DeviceService;

public interface IDeviceUnlockService
{
    public Task<List<DeviceDto>> DeviceUnlock(User user, List<IdDto> devices, Int64? ownedUserId);
}

public class DeviceUnlockService(AuthDataContext authDataContext, IMapper mapper)
    : IDeviceUnlockService
{
    public async Task<List<DeviceDto>> DeviceUnlock(
        User user,
        List<IdDto> devices,
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
            var device = user.Devices.FirstOrDefault(d => d.Id == deviceId);
            if (device == null)
            {
                throw new ForeignDeviceException("deviceDoesNotBelongToTheAccount");
            }
            device.Blocked = false;
            devicesUpdated.Add(device);
        }
        await authDataContext.SaveChangesAsync();
        return devicesUpdated.Select(mapper.Map<DeviceDto>).ToList();
    }
}
