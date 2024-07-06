namespace authServer.Exceptions;

public class ForeignUserException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
