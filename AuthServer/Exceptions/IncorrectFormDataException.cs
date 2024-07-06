namespace authServer.Exceptions;

public class IncorrectFormDataException(string message) : Exception(message)
{
    public string ErrorMessage { get; } = message;
}
