using ClassLibrary.Dtos.Auth;
using Xunit;

namespace authServer.Tests;

public class UserServiceTest : TestConfiguration.TestConfiguration
{
    [Fact]
    public async Task AddUser_ReturnsUserDtoOrThrowsActivityDeclinedException()
    {
        var result = await TestServices.TestUserService.UserCreateService.AddUser(
            TestEntities.Admin,
            new UserDto { Email = "test@test.pl" }
        );
        Assert.IsType<UserDto>(result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsUserDto()
    {
        var result = await TestServices.TestUserService.UserUpdateService.UpdateUser(
            TestEntities.Admin,
            new UserUpdateDto
            {
                FirstName = "Marian",
                CompanyName = "PADAQA",
                Id = TestEntities.OwnedUser.Id
            }
        );
        Assert.IsType<UserDto>(result);
    }

    [Fact]
    public async Task DeleteMany_ReturnsListUserDto()
    {
        var result = await TestServices.TestUserService.UserDeleteService.DeleteMany(
            TestEntities.Admin,
            new() { new() { Id = 0 } }
        );
        Assert.IsType<List<UserDto>>(result);
    }

    [Fact]
    public async Task DeleteMany_Successful()
    {
        var result = await TestServices.TestUserService.UserDeleteService.DeleteMany(
            TestEntities.Admin,
            new() { new() { Id = 0 } }
        );
        Assert.IsType<List<UserDto>>(result);
    }
}
