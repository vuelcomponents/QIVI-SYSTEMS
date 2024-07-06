namespace authServer.Exceptions;

public class TokenInvalidException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
