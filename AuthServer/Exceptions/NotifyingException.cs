namespace authServer.Exceptions;

public class NotifyingException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
