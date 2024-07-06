namespace authServer.Exceptions;

public class TokenExpiredException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
