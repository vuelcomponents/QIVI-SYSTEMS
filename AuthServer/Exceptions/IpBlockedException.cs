namespace authServer.Exceptions;

public class IpBlockedException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
