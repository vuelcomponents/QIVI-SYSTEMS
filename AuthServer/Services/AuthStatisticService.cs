using authServer.Options;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using Microsoft.Extensions.Options;

namespace authServer.Services;

public class AuthStatisticService(IOptions<JwtOptions> jwtOptions) : IAuthStatisticService
{
    public StatisticDto GetStats(User admin)
    {
        var parametersToLevel = 3;
        StatisticDto stats = new StatisticDto
        {
            UserStats = new List<UserStatDto>(),
            GlobalSecuritySettings = 0
        };
        List<User> users = admin.Users;
        users.Add(admin);
        users.ForEach(user =>
        {
            var device = user.Devices.FindAll(device =>
                device.Token != null
                && DateTime.Now.Subtract(device.Token.DateTime).TotalMinutes
                    < jwtOptions.Value.TokenExpiryMinutes
            );
            var userStat = new UserStatDto
            {
                Id = user.Id,
                Email = user.Email,
                Verified = user.Verified,
                ClaimDeviceVerification = user.ClaimDeviceVerification,
                ClaimTokenExpiryMinutes = user.ClaimTokenExpiryMinutes,
                SuppressTokenRefresh = user.SuppressTokenRefresh,
                BlockedDevicesCount = user.Devices.FindAll(d => d.Blocked).Count,
                BlockedIpsCount = user.Devices.FindAll(d => d.IpBlocked).Count,
                DevicesCount = user.Devices.Count,
                Active = device.Count > 0,
                ActiveDeviceCount = device.Count,
                MonthlyVisits = user.MonthlyVisits,
                MonthlyActivities = user.MonthlyActivities,
            };

            stats.UserStats.Add(userStat);

            if (user.Verified is true)
            {
                stats.GlobalSecuritySettings += (1.0 / users.Count) * (100.0 / parametersToLevel);
            }
            if (user.SuppressTokenRefresh)
            {
                stats.GlobalSecuritySettings += (1.0 / users.Count) * (100.0 / parametersToLevel);
            }
            if (user.ClaimDeviceVerification)
            {
                stats.GlobalSecuritySettings += (1.0 / users.Count) * (100.0 / parametersToLevel);
            }
        });

        return stats;
    }
}
