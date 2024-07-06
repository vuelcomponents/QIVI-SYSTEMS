namespace authServer.Exceptions;

public class ActiveDeviceBlockException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
