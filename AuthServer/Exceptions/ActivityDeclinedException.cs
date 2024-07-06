namespace authServer.Exceptions;

public class ActivityDeclinedException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
