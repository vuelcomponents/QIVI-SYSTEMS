namespace authServer.Exceptions;

public class AccountBlockedException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
