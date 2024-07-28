using ClassLibrary.Enums;
using RestSharp;

namespace HrTechniqueServer.Infrastructure.Clients;

public sealed class AuthClient(IHttpContextAccessor httpContextAccessor)
    : BaseClient(httpContextAccessor)
{
    protected override RestClient Client { get; } = new(Urls.GetAssociatedUrl(Urls.Name.AuthServer));
    protected override void ThrowException(Exception e)
    {
        Console.WriteLine(e.Message);
        throw new UnauthorizedAccessException();
    }
}
