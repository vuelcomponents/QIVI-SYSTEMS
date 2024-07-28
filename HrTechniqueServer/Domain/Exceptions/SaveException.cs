namespace HrTechniqueServer.Domain.Exceptions;

public class SaveException(string? message) : Exception(message)
{
    public string ErrorMessage { get; } = message!;

    public SaveException()
        : this(null)
    {
        ErrorMessage = "saveException";
    }
}
