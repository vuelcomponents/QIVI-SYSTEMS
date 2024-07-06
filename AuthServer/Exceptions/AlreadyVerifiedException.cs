namespace authServer.Exceptions;

public class AlreadyVerifiedException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
