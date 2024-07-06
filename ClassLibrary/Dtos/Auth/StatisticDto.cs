namespace ClassLibrary.Dtos.Auth;

public class StatisticDto
{
    public List<UserStatDto> UserStats { get; set; } = new List<UserStatDto>();// rozne
    public double GlobalSecuritySettings { get; set; } // wskaznik
}