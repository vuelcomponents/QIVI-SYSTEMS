using authServer.Attributes;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.LicenceRepository;
using authServer.Repositories.NotificationRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Services.AuthService.FactoryAuthService;

public abstract class BaseAuthService(
    IGettableUserRepository gettableUserRepository,
    ISettableUserRepository settableUserRepository,
    ISettableBlockedTokenRepository settableBlockedTokenRepository,
    IGettableBlockedTokenRepository gettableBlockedTokenRepository,
    IGettableLicenceRepository gettableLicenceRepository,
    ISettableSessionTokenRepository settableSessionTokenRepository,
    ISettableNotificationRepository settableNotificationRepository,
    IMapper mapper,
    ITokenBlockService tokenBlockService,
    ITokenExpiredRemovalService tokenExpiredRemovalService,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    ITokenWriteService tokenWriteService,
    IQuickActions quickActions,
    IMailerChangeEmailService mailerChangeEmailService,
    IMailerResetPasswordService mailerResetPasswordService,
    IMailerVerificationService mailerVerificationService,
    IHashService hashService,
    IOptions<JwtOptions> jwtOptions,
    IAuthDataAccessor authDataAccessor
)
{
    protected IGettableUserRepository GettableUserRepository = gettableUserRepository;
    protected ISettableUserRepository SettableUserRepository = settableUserRepository;
    protected ISettableBlockedTokenRepository SettableBlockedTokenRepository =
        settableBlockedTokenRepository;
    protected IGettableBlockedTokenRepository GettableBlockedTokenRepository =
        gettableBlockedTokenRepository;
    protected IGettableLicenceRepository GettableLicenceRepository = gettableLicenceRepository;
    protected ISettableSessionTokenRepository SettableSessionTokenRepository =
        settableSessionTokenRepository;
    protected ISettableNotificationRepository SettableNotificationRepository =
        settableNotificationRepository;
    protected IMapper Mapper = mapper;
    protected ITokenBlockService TokenBlockService = tokenBlockService;
    protected ITokenExpiredRemovalService TokenExpiredRemovalService = tokenExpiredRemovalService;
    protected ITokenReadService TokenReadService = tokenReadService;
    protected ITokenValidationService TokenValidationService = tokenValidationService;
    protected ITokenWriteService TokenWriteService = tokenWriteService;
    protected IQuickActions QuickActions = quickActions;
    protected readonly IMailerChangeEmailService MailerChangeEmailService =
        mailerChangeEmailService;
    protected readonly IMailerResetPasswordService MailerResetPasswordService =
        mailerResetPasswordService;
    protected readonly IMailerVerificationService MailerVerificationService =
        mailerVerificationService;
    protected IHashService HashService = hashService;
    protected IOptions<JwtOptions> JwtOptions = jwtOptions;
    protected IAuthDataAccessor AuthDataAccessor = authDataAccessor;
}
