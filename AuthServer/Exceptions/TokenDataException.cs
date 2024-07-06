namespace authServer.Exceptions;

public class TokenDataException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
