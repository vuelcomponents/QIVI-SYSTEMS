namespace authServer.Exceptions;

public class ForeignDeviceException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
