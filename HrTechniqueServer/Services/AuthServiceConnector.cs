using ClassLibrary.Enums;
using Newtonsoft.Json;
using RestSharp;

namespace HrTechniqueServer.Services;

public sealed class AuthServiceConnector(IHttpContextAccessor httpContextAccessor)
    : IAuthServiceConnector
{
    private readonly RestClient _client = new(Urls.GetAssociatedUrl(Urls.Name.AuthServer));

    public T GetSync<T>(string path)
        where T : class
    {
        var request = new RestRequest($"{path}");

        var r = CollectRequestSensitiveData(request, path);

        var response = _client.Execute(r);

        if (response.ErrorException != null)
        {
            Console.WriteLine(response.ErrorException.Message);
            throw new UnauthorizedAccessException();
        }
        var responseData = response.Content!;
        return JsonConvert.DeserializeObject<T>(responseData)!;
    }

    public async Task<T> Get<T>(string path)
        where T : class
    {
        var request = new RestRequest($"{path}");

        var r = CollectRequestSensitiveData(request, path);

        var response = await _client.ExecuteAsync(r);

        if (response.ErrorException != null)
        {
            throw new UnauthorizedAccessException();
        }
        var responseData = response.Content!;
        return JsonConvert.DeserializeObject<T>(responseData)!;
    }

    public async Task Call(string path)
    {
        var request = new RestRequest($"{path}");

        var r = CollectRequestSensitiveData(request, path);

        var response = await _client.ExecuteAsync(r);

        if (response.ErrorException != null)
        {
            throw new UnauthorizedAccessException();
        }
    }

    private RestRequest CollectRequestSensitiveData(RestRequest request, string path)
    {
        var context = httpContextAccessor.HttpContext;
        var contextRequest = context!.Request;
        var cookiePath = $"/{path}";
        var cookieDomain = contextRequest.Host.Host;
        var ipAddress = context.Connection.RemoteIpAddress!.ToString();
        var userAgent = contextRequest.Headers["User-Agent"].ToString();
        var cookies = contextRequest.Cookies;
        foreach (var cookie in cookies)
        {
            request.AddCookie(cookie.Key, cookie.Value, cookiePath, cookieDomain);
        }
        request.AddHeader("X-Forwarded-For", ipAddress);
        request.AddHeader("User-Agent", userAgent);
        return request;
    }
}
