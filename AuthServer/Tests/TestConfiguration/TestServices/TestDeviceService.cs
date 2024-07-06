using authServer.Data;
using authServer.Options;
using authServer.Services.DeviceService;
using authServer.Services.DeviceService.FactoryDeviceService;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace authServer.Tests.TestConfiguration.TestServices;

public class TestDeviceService
{
    public readonly IDeviceBlockService DeviceBlockService;
    public readonly IDeviceRetrievalService DeviceRetrievalService;
    public readonly IDeviceSignoutService DeviceSignoutService;
    public readonly IDeviceUnlockService DeviceUnlockService;
    public readonly IDeviceVerificationService DeviceVerificationService;
    public readonly IIpBlockService IpBlockService;
    public readonly IIpUnlockService IpUnlockService;

    public TestDeviceService(
        TestTokenService testTokenService,
        AuthDataContext testContext,
        IMapper mapper,
        TestMailerService testMailerService,
        IOptions<JwtOptions> jwtOptions
    )
    {
        IFactoryDeviceService factoryDeviceService = new FactoryDeviceService(
            testTokenService.TokenBlockService,
            testTokenService.TokenExpiredRemovalService,
            testTokenService.TokenReadService,
            testTokenService.TokenValidationService,
            testTokenService.TokenWriteService,
            testContext,
            mapper,
            testMailerService.MailerChangeEmailService,
            testMailerService.MailerResetPasswordService,
            testMailerService.MailerVerificationService,
            jwtOptions
        );
        DeviceBlockService = factoryDeviceService.CreateDeviceBlockService();
        DeviceRetrievalService = factoryDeviceService.CreateDeviceRetrievalService();
        DeviceSignoutService = factoryDeviceService.CreateDeviceSignoutService();
        DeviceUnlockService = factoryDeviceService.CreateDeviceUnlockService();
        DeviceVerificationService = factoryDeviceService.CreateDeviceVerificationService();
        IpBlockService = factoryDeviceService.CreateIpBlockService();
        IpUnlockService = factoryDeviceService.CreateIpUnlockService();
    }
}
