using authServer.Data;
using authServer.Options;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace authServer.Services.DeviceService.FactoryDeviceService;

public abstract class BaseDeviceService(
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
{
    protected readonly AuthDataContext AuthDataContext = dbContext;
    protected readonly ITokenBlockService TokenBlockService = tokenBlockService;
    protected readonly ITokenExpiredRemovalService TokenExpiredRemovalService =
        tokenExpiredRemovalService;
    protected readonly ITokenReadService TokenReadService = tokenReadService;
    protected readonly ITokenValidationService TokenValidationService = tokenValidationService;
    protected readonly ITokenWriteService TokenWriteService = tokenWriteService;
    protected readonly IMapper Mapper = mapper;
    protected readonly IMailerChangeEmailService MailerChangeEmailService =
        mailerChangeEmailService;
    protected readonly IMailerResetPasswordService MailerResetPasswordService =
        mailerResetPasswordService;
    protected readonly IMailerVerificationService MailerVerificationService =
        mailerVerificationService;
    protected readonly IOptions<JwtOptions> JwtOptions = jwtOptions;
}
