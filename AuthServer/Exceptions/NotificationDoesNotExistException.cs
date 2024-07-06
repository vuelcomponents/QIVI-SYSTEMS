namespace authServer.Exceptions;

public class NotificationDoesNotExistException(string? message) : Exception(message)
{
    public string ErrorMessage { get; } = message!;

    public NotificationDoesNotExistException()
        : this(null)
    {
        ErrorMessage = "notificationDoesNotExist";
    }
}
