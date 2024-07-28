using Newtonsoft.Json;
using RestSharp;

namespace HrTechniqueServer.Infrastructure.Clients;

public abstract class BaseClient(IHttpContextAccessor httpContextAccessor)
{
    protected abstract RestClient Client { get; }
    
    public virtual T GetSync<T>(string path, Action<Exception>? onException = null)
    {
        var request = new RestRequest($"{path}");

        var r = CollectRequestSensitiveData(request, path);

        var response = Client.Execute(r);

        if (response.ErrorException != null)
        {
            if (onException != null)
            {
                onException(response.ErrorException);
                return default!;
            }
            ThrowException(response.ErrorException);
        }
        var responseData = response.Content!;
        return JsonConvert.DeserializeObject<T>(responseData)!;
    }

    public virtual async Task<T> Get<T>(string path, Action<Exception>? onException = null)
    {
        var request = new RestRequest($"{path}");

        var r = CollectRequestSensitiveData(request, path);

        var response = await Client.ExecuteAsync(r);

        if (response.ErrorException != null)
        {
            if (onException != null)
            {
                onException(response.ErrorException);
                return default!;
            }
            ThrowException(response.ErrorException);
        }
        var responseData = response.Content!;
        return JsonConvert.DeserializeObject<T>(responseData)!;
    }

    public virtual async Task Call(string path, Action<Exception>? onException = null)
    {
        var request = new RestRequest($"{path}");

        var r = CollectRequestSensitiveData(request, path);

        var response = await Client.ExecuteAsync(r);

        if (response.ErrorException != null)
        {
            if (onException != null)
            {
               onException(response.ErrorException);
               return;
            }
            ThrowException(response.ErrorException);
        }
    }
    protected virtual void ThrowException(Exception e)
    {
        Console.WriteLine(e.Message);
        throw e;
    }
    protected virtual RestRequest CollectRequestSensitiveData(RestRequest request, string path)
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