namespace authServer.Exceptions;

public class ActiveIpBlockException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
