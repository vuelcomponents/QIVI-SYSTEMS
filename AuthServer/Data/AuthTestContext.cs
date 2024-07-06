using authServer.Options;
using authServer.Services;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace authServer.Data;

public class AuthTestContext(IOptions<ConnectionStrings> connectionStrings)
    : AuthDataContext(connectionStrings)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        HashService hashService = new HashService();
        hashService.Hash(
            TestData.TestUserPassword,
            out byte[] passwordHash,
            out byte[] passwordSalt
        );
        modelBuilder.HasSequence<int>("StartFromUserSeeds", schema: "dbo").StartsAt(5);
        modelBuilder
            .Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEXT VALUE FOR dbo.StartFromUserSeeds");

        modelBuilder
            .Entity<Gender>()
            .HasData(
                new Gender { Id = 1, Code = "Man" },
                new Gender { Id = 2, Code = "Woman" },
                new Gender { Id = 3, Code = "Other" }
            );
        modelBuilder
            .Entity<User>()
            .HasData(
                new User
                {
                    Id = 1,
                    MaxUsersCount = 20,
                    Email = TestData.TestUserEmail,
                    FirstName = "Robert",
                    LastName = "Haze",
                    Password = passwordHash,
                    Salt = passwordSalt,
                    Address = "super-admin-test-address",
                    HrtLicence = true,
                    Role = Role.SUPERADMIN,
                    LombanditLicence = true,
                    Verified = true
                },
                new User
                {
                    Id = 2,
                    MaxUsersCount = 20,
                    Email = TestData.TestEmailReceiver,
                    Password = passwordHash,
                    FirstName = "Jarek",
                    LastName = "Blacksmith",
                    Salt = passwordSalt,
                    Address = "user-test-address",
                    HrtLicence = true,
                    Role = Role.USER,
                    LombanditLicence = true,
                    UserId = 1,
                    Verified = true
                },
                new User
                {
                    Id = 3,
                    MaxUsersCount = 20,
                    FirstName = "Marzena",
                    LastName = "Pijak",
                    Email = "BlockedIpEmaiL@email.com",
                    Password = passwordHash,
                    Salt = passwordSalt,
                    Address = "blocked-ip-test-address",
                    HrtLicence = true,
                    Role = Role.USER,
                    LombanditLicence = true,
                    UserId = 1,
                    Verified = true,
                },
                new User
                {
                    Id = 4,
                    MaxUsersCount = 20,
                    Email = "BlockedDeviceEmaiL@email.com",
                    Password = passwordHash,
                    Salt = passwordSalt,
                    FirstName = "Karol",
                    LastName = "Ujeb",
                    Address = "blocked-device-test-address",
                    HrtLicence = true,
                    Role = Role.USER,
                    LombanditLicence = true,
                    UserId = 1,
                    Verified = true,
                }
            );
        modelBuilder.HasSequence<int>("StartFromDeviceSeeds", schema: "dbo").StartsAt(5);
        modelBuilder
            .Entity<Device>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEXT VALUE FOR dbo.StartFromDeviceSeeds");
        modelBuilder
            .Entity<Device>()
            .HasData(
                new Device
                {
                    Ip = "0.0.0.0",
                    UserAgent = "SomeUserAgent",
                    Id = 1,
                    UserId = 3,
                    IpBlocked = true
                },
                new Device
                {
                    Ip = "1.1.0.0",
                    UserAgent = "BlockedUserAgent",
                    Id = 2,
                    UserId = 4,
                    Blocked = true
                },
                new Device
                {
                    Ip = "1.1.0.0",
                    UserAgent = "BlockedUserAgent",
                    Id = 3,
                    UserId = 1,
                    Blocked = true
                },
                new Device
                {
                    Ip = "1.1.0.0",
                    UserAgent = "NormalUserAgent",
                    Id = 4,
                    UserId = 2,
                    Blocked = false
                }
            );
    }
}
