namespace HrTechniqueServer.Services;

public interface IAuthServiceConnector
{
    T GetSync<T>(string path)
        where T : class;

    Task<T> Get<T>(string path)
        where T : class;
    Task Call(string path);
}
