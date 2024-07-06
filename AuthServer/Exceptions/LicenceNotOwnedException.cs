namespace authServer.Exceptions;

public class LicenceNotOwnedException : Exception
{
    public string ErrorMessage { get; } = "licenceNotOwnedByAdmin";
}
