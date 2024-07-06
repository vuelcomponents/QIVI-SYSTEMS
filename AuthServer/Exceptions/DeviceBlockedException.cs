namespace authServer.Exceptions;

public class DeviceBlockedException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
