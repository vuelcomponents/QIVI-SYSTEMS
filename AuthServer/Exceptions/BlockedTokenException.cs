namespace authServer.Exceptions;

public class BlockedTokenException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
