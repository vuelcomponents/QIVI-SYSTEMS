using authServer.Data;
using authServer.Exceptions;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Models;

namespace authServer.Services.DeviceService;

public interface IIpUnlockService
{
    Task<List<DeviceDto>> IpUnlock(User user, List<IdDto> devices, Int64? ownedUserId);
}

public class IpUnlockService(AuthDataContext authDataContext, IMapper mapper) : IIpUnlockService
{
    public async Task<List<DeviceDto>> IpUnlock(User user, List<IdDto> devices, Int64? ownedUserId)
    {
        if (ownedUserId != null)
        {
            User? ownedUser = user.Users.FirstOrDefault(u => u.Id == ownedUserId);
            user = ownedUser ?? throw new ForeignUserException("userDoesNotBelongToTheAccount");
        }
        List<Device> devicesUpdated = new List<Device>();
        foreach (var idDto in devices)
        {
            var deviceId = idDto.Id;
            var device = user.Devices.FirstOrDefault(d => d.Id == deviceId);
            if (device == null)
            {
                throw new ForeignDeviceException("deviceDoesNotBelongToTheAccount");
            }

            List<Device> allDevices = user.Devices.FindAll(d => d.Ip == device.Ip).ToList();
            foreach (var allDevice in allDevices)
            {
                if (devicesUpdated.Any(d => d.Id == allDevice.Id))
                {
                    continue;
                }
                if (allDevice.Token != null)
                {
                    authDataContext.SessionTokens.Remove(allDevice.Token);
                }
                allDevice.IpBlocked = false;
                devicesUpdated.Add(allDevice);
            }
        }
        await authDataContext.SaveChangesAsync();
        return devicesUpdated.Select(mapper.Map<DeviceDto>).ToList();
    }
}
