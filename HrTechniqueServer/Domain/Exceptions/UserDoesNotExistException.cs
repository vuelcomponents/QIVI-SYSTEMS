namespace HrTechniqueServer.Domain.Exceptions;

public class UserDoesNotExistException(string? message) : Exception(message)
{
    public string ErrorMessage { get; } = message!;

    public UserDoesNotExistException()
        : this(null)
    {
        ErrorMessage = "userDoesNotExist";
    }
}
