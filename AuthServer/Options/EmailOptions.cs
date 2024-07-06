namespace authServer.Options;

public class EmailOptions
{
    public required string AuthMailAddress { get; set; }
    public required string AuthMailSmtp { get; set; }
    public required Int16 AuthMailTLS { get; set; }

    //MailerKitDev2004#
    public required string AuthMailPassword { get; set; }
}
