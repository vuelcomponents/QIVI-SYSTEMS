using authServer.Data;
using authServer.Options;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace authServer.Services.DeviceService.FactoryDeviceService;

public class FactoryDeviceService(
    ITokenBlockService tokenBlockService,
    ITokenExpiredRemovalService tokenExpiredRemovalService,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    ITokenWriteService tokenWriteService,
    AuthDataContext dbContext,
    IMapper mapper,
    IMailerChangeEmailService mailerChangeEmailService,
    IMailerResetPasswordService mailerResetPasswordService,
    IMailerVerificationService mailerVerificationService,
    IOptions<JwtOptions> jwtOptions
)
    : BaseDeviceService(
        tokenBlockService,
        tokenExpiredRemovalService,
        tokenReadService,
        tokenValidationService,
        tokenWriteService,
        dbContext,
        mapper,
        mailerChangeEmailService,
        mailerResetPasswordService,
        mailerVerificationService,
        jwtOptions
    ),
        IFactoryDeviceService
{
    public IDeviceBlockService CreateDeviceBlockService()
    {
        return new DeviceBlockService(AuthDataContext, Mapper);
    }

    public IDeviceRetrievalService CreateDeviceRetrievalService()
    {
        return new DeviceRetrievalService(Mapper);
    }

    public IDeviceSignoutService CreateDeviceSignoutService()
    {
        return new DeviceSignoutService(TokenBlockService, Mapper);
    }

    public IDeviceUnlockService CreateDeviceUnlockService()
    {
        return new DeviceUnlockService(AuthDataContext, Mapper);
    }

    public IDeviceVerificationService CreateDeviceVerificationService()
    {
        return new DeviceVerificationService(
            AuthDataContext,
            JwtOptions,
            MailerVerificationService,
            TokenReadService,
            TokenValidationService,
            TokenWriteService,
            Mapper
        );
    }

    public IIpBlockService CreateIpBlockService()
    {
        return new IpBlockService(AuthDataContext, Mapper);
    }

    public IIpUnlockService CreateIpUnlockService()
    {
        return new IpUnlockService(AuthDataContext, Mapper);
    }
}
