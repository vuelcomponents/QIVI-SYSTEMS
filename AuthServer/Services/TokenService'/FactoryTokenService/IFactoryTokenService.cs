namespace authServer.Services.TokenService_.FactoryTokenService;

public interface IFactoryTokenService
{
    ITokenBlockService CreateTokenBlockService();
    ITokenExpiredRemovalService CreateTokenExpiredRemovalService();
    ITokenReadService CreateTokenReadService();
    ITokenValidationService CreateTokenValidationService();
    ITokenWriteService CreateTokenWriteService();
}
