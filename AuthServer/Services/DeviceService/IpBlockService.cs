using authServer.Data;
using authServer.Exceptions;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Models;

namespace authServer.Services.DeviceService;

public interface IIpBlockService
{
    public Task<List<DeviceDto>> IpBlock(
        User user,
        List<IdDto> devices,
        Device currentDevice,
        string currentIpAddress,
        Int64? ownedUserId
    );
}

public class IpBlockService(AuthDataContext authDataContext, IMapper mapper) : IIpBlockService
{
    public async Task<List<DeviceDto>> IpBlock(
        User user,
        List<IdDto> devices,
        Device currentDevice,
        string currentIpAddress,
        Int64? ownedUserId
    )
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
            if (ownedUserId == null)
            {
                if (device.Ip == currentIpAddress)
                {
                    throw new ActiveIpBlockException("cannotBanOwnerIpAddress");
                }
            }
            if (device.IpBlocked)
            {
                throw new Exception("ipAlreadyBlocked");
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
                allDevice.IpBlocked = true;
                devicesUpdated.Add(allDevice);
            }
        }
        await authDataContext.SaveChangesAsync();
        return devicesUpdated.Select(mapper.Map<DeviceDto>).ToList();
    }
}
