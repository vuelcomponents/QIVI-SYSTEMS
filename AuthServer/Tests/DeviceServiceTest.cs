using authServer.Exceptions;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Models;
using Xunit;

namespace authServer.Tests;

public class DeviceServiceTest : TestConfiguration.TestConfiguration
{
    [Fact]
    public async Task VerifyDeviceFromMailLink_IsSuccessful()
    {
        TestServices.TestTokenService.TokenWriteService.WriteDeviceVerificationToken(
            TestEntities.OwnedUser,
            out string token
        );
        await TestServices.TestDeviceService.DeviceVerificationService.VerifyDeviceFromMailLink(
            TestEntities.OwnedUserDevice.Id,
            token
        );
    }

    [Fact]
    public async Task VerifyDeviceFromMailLink_ThrowsDeviceDoesNotExistException_WhenForeignDeviceId()
    {
        TestServices.TestTokenService.TokenWriteService.WriteDeviceVerificationToken(
            TestEntities.OwnedUser,
            out string token
        );
        await Assert.ThrowsAsync<DeviceDoesNotExistException>(
            () =>
                TestServices.TestDeviceService.DeviceVerificationService.VerifyDeviceFromMailLink(
                    TestEntities.OwnedUserDevice.Id + 50,
                    token
                )
        );
    }

    [Fact]
    public async Task VerifyDeviceByAdmin_ReturnsListDeviceDto()
    {
        var result =
            await TestServices.TestDeviceService.DeviceVerificationService.VerifyDeviceByAdmin(
                TestEntities.Admin,
                TestEntities.OwnedUser.Id,
                [new IdDto { Id = TestEntities.OwnedUserDevice.Id }]
            );
        Assert.IsType<List<DeviceDto>>(result);
    }

    [Fact]
    public async Task VerifyDeviceByAdmin_ThrowsAlreadyVerifiedException()
    {
        await TestServices.TestDeviceService.DeviceVerificationService.VerifyDeviceByAdmin(
            TestEntities.Admin,
            TestEntities.OwnedUser.Id,
            [new IdDto { Id = TestEntities.OwnedUserDevice.Id }]
        );
        await Assert.ThrowsAsync<AlreadyVerifiedException>(
            () =>
                TestServices.TestDeviceService.DeviceVerificationService.VerifyDeviceByAdmin(
                    TestEntities.Admin,
                    TestEntities.OwnedUser.Id,
                    [new IdDto { Id = TestEntities.OwnedUserDevice.Id }]
                )
        );
    }

    [Fact]
    public async Task InitVerifyDeviceUser_Successful_ThrowsNotifyingException()
    {
        await Assert.ThrowsAsync<NotifyingException>(
            () =>
                TestServices.TestDeviceService.DeviceVerificationService.InitVerifyDeviceUser(
                    TestEntities.OwnedUser,
                    [new IdDto { Id = TestEntities.OwnedUserDevice.Id }]
                )
        );
    }

    [Fact]
    public async Task DeviceSignout_Self_ReturnsDeviceDto()
    {
        var result = await TestServices.TestDeviceService.DeviceSignoutService.DeviceSignout(
            TestEntities.Admin,
            TestEntities.AdminDevice.Id,
            null
        );
        Assert.IsType<DeviceDto>(result);
    }

    [Fact]
    public async Task DeviceSignout_OwnedUser_ReturnsDeviceDto()
    {
        var result = await TestServices.TestDeviceService.DeviceSignoutService.DeviceSignout(
            TestEntities.Admin,
            TestEntities.OwnedUserDevice.Id,
            TestEntities.OwnedUser.Id
        );
        Assert.IsType<DeviceDto>(result);
    }

    [Fact]
    public async Task DeviceBlock_Self_ReturnsListDeviceDto()
    {
        var result = await TestServices.TestDeviceService.DeviceBlockService.DeviceBlock(
            TestEntities.OwnedUser,
            [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
            new Device
            {
                Id = 10,
                Ip = "",
                UserAgent = ""
            },
            null
        );
        Assert.IsType<List<DeviceDto>>(result);
    }

    [Fact]
    public async Task DeviceBlock_OwnedUser_ReturnsListDeviceDto()
    {
        var result = await TestServices.TestDeviceService.DeviceBlockService.DeviceBlock(
            TestEntities.Admin,
            [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
            new Device
            {
                Id = 10,
                Ip = "",
                UserAgent = ""
            },
            TestEntities.OwnedUser.Id
        );
        Assert.IsType<List<DeviceDto>>(result);
    }

    [Fact]
    public async Task DeviceBlock_Self_ThrowsActiveDeviceBlockException()
    {
        await Assert.ThrowsAsync<ActiveDeviceBlockException>(
            () =>
                TestServices.TestDeviceService.DeviceBlockService.DeviceBlock(
                    TestEntities.OwnedUser,
                    [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
                    TestEntities.OwnedUserDevice,
                    null
                )
        );
    }

    [Fact]
    public async Task DeviceBlock_OwnedUser_ThrowsActiveDeviceBlockException()
    {
        await Assert.ThrowsAsync<ActiveDeviceBlockException>(
            () =>
                TestServices.TestDeviceService.DeviceBlockService.DeviceBlock(
                    TestEntities.Admin,
                    [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
                    TestEntities.OwnedUserDevice,
                    TestEntities.OwnedUser.Id
                )
        );
    }

    [Fact]
    public async Task DeviceUnlock_Self_ReturnsListDeviceDto()
    {
        var result = await TestServices.TestDeviceService.DeviceUnlockService.DeviceUnlock(
            TestEntities.OwnedUser,
            [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
            null
        );
        Assert.IsType<List<DeviceDto>>(result);
    }

    [Fact]
    public async Task DeviceUnlock_OwnedUser_ReturnsListDeviceDto()
    {
        var result = await TestServices.TestDeviceService.DeviceUnlockService.DeviceUnlock(
            TestEntities.Admin,
            [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
            TestEntities.OwnedUser.Id
        );
        Assert.IsType<List<DeviceDto>>(result);
    }

    [Fact]
    public async Task DeviceUnlock_Self_ThrowsForeignDeviceException_WhenDeviceIsNotPresentInUserDevices()
    {
        await Assert.ThrowsAsync<ForeignDeviceException>(
            () =>
                TestServices.TestDeviceService.DeviceUnlockService.DeviceUnlock(
                    TestEntities.OwnedUser,
                    [new IdDto { Id = TestEntities.OwnedUserDevice.Id + 20 }],
                    null
                )
        );
    }

    [Fact]
    public async Task DeviceUnlock_OwnedUser_ThrowsForeignDeviceException_WhenDeviceIsNotPresentInUserDevices()
    {
        await Assert.ThrowsAsync<ForeignDeviceException>(
            () =>
                TestServices.TestDeviceService.DeviceUnlockService.DeviceUnlock(
                    TestEntities.Admin,
                    [new IdDto { Id = TestEntities.OwnedUserDevice.Id + 20 }],
                    TestEntities.OwnedUser.Id
                )
        );
    }

    [Fact]
    public async Task IpBlock_Self_ReturnsListDeviceDto()
    {
        var result = await TestServices.TestDeviceService.IpBlockService.IpBlock(
            TestEntities.OwnedUser,
            [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
            new Device
            {
                Id = 10,
                Ip = "",
                UserAgent = ""
            },
            "0.0.0.0",
            null
        );
        Assert.IsType<List<DeviceDto>>(result);
    }

    [Fact]
    public async Task IpBlock_OwnedUser_ReturnsListDeviceDto()
    {
        var result = await TestServices.TestDeviceService.DeviceBlockService.DeviceBlock(
            TestEntities.Admin,
            [new IdDto { Id = TestEntities.OwnedUserDevice.Id }],
            new Device
            {
                Id = 10,
                Ip = "22.2.2.2",
                UserAgent = ""
            },
            TestEntities.OwnedUser.Id
        );
        Assert.IsType<List<DeviceDto>>(result);
    }
}
