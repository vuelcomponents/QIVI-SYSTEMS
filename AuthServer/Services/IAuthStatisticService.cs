using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;

namespace authServer.Services;

public interface IAuthStatisticService
{
    StatisticDto GetStats(User admin);
}
