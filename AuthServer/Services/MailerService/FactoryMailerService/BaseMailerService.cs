using authServer.Options;
using Microsoft.Extensions.Options;

namespace authServer.Services.MailerService.FactoryMailerService;

public class BaseMailerService(IOptions<EmailOptions> options)
{
    protected IOptions<EmailOptions> Options = options;
}
