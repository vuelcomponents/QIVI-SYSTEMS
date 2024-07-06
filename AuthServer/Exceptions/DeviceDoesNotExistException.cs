namespace authServer.Exceptions;

public class DeviceDoesNotExistException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
