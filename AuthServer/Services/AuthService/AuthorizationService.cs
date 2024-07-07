using System.Text;
using authServer.Attributes;
using authServer.Exceptions;
using authServer.Options;
using authServer.Repositories.SessionTokenRepository;
using authServer.Repositories.UserRepository;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using Microsoft.Extensions.Options;

namespace authServer.Services.AuthService;

public interface IAuthorizationService
{
    Task<AuthResponse> Authorized(string token, long id);
}

public class AuthorizationService(
    IUserRepository userRepository,
    ISessionTokenRepository sessionTokenRepository,
    IMapper mapper,
    ITokenBlockService tokenBlockService,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    ITokenWriteService tokenWriteService,
    IOptions<JwtOptions> jwtOptions,
    IAuthDataAccessor authDataAccessor
) : IAuthorizationService
{
    public async Task<AuthResponse> Authorized(string token, long id)
    {
        var dbUser =
            await userRepository.GetAsync(u=> u.Id .Equals(Convert.ToInt64(id)))
            ?? throw new UnauthorizedAccessException();

        TimeSpan expirationTimeToken = tokenReadService.GetTimeToExpiration(token);

        bool isClaimTokenExpInRangeFor100Sure = dbUser.ClaimTokenExpiryMinutes is <= 120 and > 0;
        if (isClaimTokenExpInRangeFor100Sure)
        {
            bool isTokenExpiredDueToPersonalSettings =
                expirationTimeToken.TotalMinutes
                <= jwtOptions.Value.TokenExpiryMinutes - dbUser.ClaimTokenExpiryMinutes;
            if (isTokenExpiredDueToPersonalSettings)
            {
                await tokenBlockService.Block(token);
                if (dbUser.RefreshToken != null)
                    await tokenBlockService.BlockRefresh(
                        Encoding.UTF8.GetString(dbUser.RefreshToken)
                    );

                throw new UnauthorizedAccessException();
            }
        }

        RefreshTokenIfPossible(
            Encoding.UTF8.GetString(dbUser.RefreshToken ?? Encoding.UTF8.GetBytes("")),
            ref token,
            expirationTimeToken,
            isClaimTokenExpInRangeFor100Sure,
            dbUser,
            out bool changed
        );

        UserDto UserWithFreshLoginDateTime()
        {
            var userDto = mapper.Map<UserDto>(dbUser);
            userDto.SignedInDateTime = authDataAccessor.Device!.SignedInDateTime;
            userDto.Notifications = userDto
                .Notifications.Where(n => !n.Seen)
                .Select(mapper.Map<NotificationDto>)
                .OrderByDescending(n => n.TimeStamp)
                .ToList();
            userDto.Notified = userDto.Notifications.Count > 0;
            return userDto;
        }

        return new AuthResponse
        {
            User = UserWithFreshLoginDateTime(),
            Token = changed ? token : null
        };
    }

    private void RefreshTokenIfPossible(
        string headerRefreshToken,
        ref string token,
        TimeSpan expirationTimeToken,
        bool isClaimTokenExpInRangeFor100Sure,
        User dbUser,
        out bool changed
    )
    {
        changed = false;
        if (tokenValidationService.IsValidRefreshToken(headerRefreshToken))
        {
            bool isTheTimeToRefreshToken =
                expirationTimeToken.TotalMinutes < jwtOptions.Value.MinutesToRefresh
                || (
                    expirationTimeToken.TotalMinutes
                        < jwtOptions.Value.TokenExpiryMinutes - dbUser.ClaimTokenExpiryMinutes * 0.7
                    && isClaimTokenExpInRangeFor100Sure
                );
            if (isTheTimeToRefreshToken && !dbUser.SuppressTokenRefresh)
            {
                tokenWriteService.WriteToken(dbUser, out string newToken, out _);

                var device = authDataAccessor.Device!;
                sessionTokenRepository.DeleteMany([device.Token!]);

                device.Token = new SessionToken { Token = newToken, DateTime = DateTime.Now, };
                device.SignedInDateTime = DateTime.Now;
                dbUser.MonthlyActivities++;
                sessionTokenRepository.Save();

                token = $"{newToken}";
                changed = true;
                Console.WriteLine("Tokent refreshed");
            }
        }
        else
        {
            throw new TokenInvalidException("invalidAuthorizationSecondaryKey");
        }
    }
}
