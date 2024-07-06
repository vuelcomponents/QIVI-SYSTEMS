namespace authServer.Options;

public class JwtOptions
{
    public required string SecretTokenKey { get; set; }
    public required string SecretRefreshTokenTokenKey { get; set; }
    public required string SecretVerificationTokenKey { get; set; }
    public required string SecretResetPasswordTokenKey { get; set; }
    public required string SecretDeviceVerifyTokenKey { get; set; }
    public required string SecretChangeEmailTokenKey { get; set; }
    public required string SecretTemporaryPassword { get; set; }
    public required int MinutesToRefresh { get; set; }
    public required int RefreshTokenExpiryMinutes { get; set; }
    public required int VerificationTokenExpiryMinutes { get; set; }
    public required int ResetPasswordTokenExpiryMinutes { get; set; }
    public required int DeviceVerificationTokenExpiryMinutes { get; set; }
    public required int ChangeEmailTokenExpiryMinutes { get; set; }
    public required int TokenExpiryMinutes { get; set; }
}
