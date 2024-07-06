namespace HrTechniqueServer.Exceptions;

public class EmployeeDoesNotExistException(string? message) : Exception(message)
{
    public string ErrorMessage { get; } = message!;

    public EmployeeDoesNotExistException()
        : this(null)
    {
        ErrorMessage = "productDoesNotExist";
    }
}
