using authServer.Services;
using ClassLibrary.Utils;
using Moq;

namespace authServer.Tests.TestConfiguration.TestTools;

public class TestTools
{
    public readonly Mock<IQuickActions> QuickActions = new();
    public readonly IHashService HashService = new HashService();
}
