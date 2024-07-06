namespace authServer.Services.DeviceService.FactoryDeviceService;

public interface IFactoryDeviceService
{
    IDeviceBlockService CreateDeviceBlockService();
    IDeviceRetrievalService CreateDeviceRetrievalService();
    IDeviceSignoutService CreateDeviceSignoutService();
    IDeviceUnlockService CreateDeviceUnlockService();
    IDeviceVerificationService CreateDeviceVerificationService();
    IIpBlockService CreateIpBlockService();
    IIpUnlockService CreateIpUnlockService();
}
