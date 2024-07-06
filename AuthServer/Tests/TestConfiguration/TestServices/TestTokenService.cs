using authServer.Options;
using authServer.Services.TokenService_;
using authServer.Services.TokenService_.FactoryTokenService;
using Microsoft.Extensions.Options;

namespace authServer.Tests.TestConfiguration.TestServices
{
    public class TestTokenService
    {
        public readonly ITokenBlockService TokenBlockService;
        public readonly ITokenExpiredRemovalService TokenExpiredRemovalService;
        public readonly ITokenReadService TokenReadService;
        public readonly ITokenValidationService TokenValidationService;
        public readonly ITokenWriteService TokenWriteService;

        public TestTokenService(
            TestRepositories.TestRepositories testRepositories,
            IOptions<JwtOptions> jwtOptions
        )
        {
            IFactoryTokenService factoryTokenService = new FactoryTokenService(
                testRepositories.GettableSessionTokenRepository,
                testRepositories.SettableSessionTokenRepository,
                jwtOptions,
                testRepositories.GettableBlockedTokenRepository,
                testRepositories.SettableBlockedTokenRepository
            );
            TokenBlockService = factoryTokenService.CreateTokenBlockService();
            TokenExpiredRemovalService = factoryTokenService.CreateTokenExpiredRemovalService();
            TokenReadService = factoryTokenService.CreateTokenReadService();
            TokenValidationService = factoryTokenService.CreateTokenValidationService();
            TokenWriteService = factoryTokenService.CreateTokenWriteService();
        }
    }
}
