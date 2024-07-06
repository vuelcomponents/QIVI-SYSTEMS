using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace authServer.Data;

public class BaseAuthDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<BlockedToken> BlockedTokens { get; set; }
    public DbSet<BlockedRefreshToken> BlockedRefreshTokens { get; set; }
    public DbSet<BlockedVerifyToken> BlockedVerifyTokens { get; set; }
    public DbSet<SessionToken> SessionTokens { get; set; }
    public DbSet<BlockedResetPasswordToken> BlockedResetPasswordTokens { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<BlockedDeviceVerifyToken> BlockedDeviceVerifyTokens { get; set; }
    public DbSet<BlockedChangeEmailToken> BlockedChangeEmailTokens { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Licence> Licences { get; set; }
}
