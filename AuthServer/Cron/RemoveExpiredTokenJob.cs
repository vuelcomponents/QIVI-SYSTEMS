using authServer.Services;
using authServer.Services.TokenService_;
using Quartz;

namespace authServer.Cron;

public sealed class RemoveExpiredTokenJob(ITokenExpiredRemovalService tokenExpiredRemovalService)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await tokenExpiredRemovalService.RemoveExpiredTokens();
        await tokenExpiredRemovalService.RemoveExpiredSessionTokens();
        await tokenExpiredRemovalService.RemoveExpiredRefreshTokens();
        await tokenExpiredRemovalService.RemoveExpiredVerifyTokens();
        await tokenExpiredRemovalService.RemoveExpiredResetPasswordTokens();
        await tokenExpiredRemovalService.RemoveExpiredDeviceVerifyTokens();
    }
}
