namespace HrTechniqueServer.Domain.Exceptions;

public class IdNotProvidedException(string? message) : Exception(message)
{
    public string ErrorMessage { get; } = message!;

    public IdNotProvidedException()
        : this(null)
    {
        ErrorMessage = "idHasNotBeenProvided";
    }
}
