using authServer.Data;
using ClassLibrary.Models;

namespace authServer.Tests.TestConfiguration.TestEntities;

public class TestEntities(AuthDataContext testContext)
{
    public readonly User Admin = testContext.Users.FirstOrDefault(u => u.Id == 1)!;
    public readonly User OwnedUser = testContext.Users.FirstOrDefault(u => u.Id == 2)!;
    public readonly Device AdminDevice = testContext.Devices.FirstOrDefault(d => d.Id == 3)!;
    public readonly Device OwnedUserDevice = testContext.Devices.FirstOrDefault(d => d.Id == 4)!;
}
