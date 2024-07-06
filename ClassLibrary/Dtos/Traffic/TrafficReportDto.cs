using ClassLibrary.Dtos.Auth;
using ClassLibrary.Enums;

namespace ClassLibrary.Dtos.Traffic;

public class TrafficReportDto
{
    public long? Id { get; set; }
    public Urls.Name Service { get; set; }
    public DeviceDto? Device { get; set; }
    public string? Ip { get; set; }
    public DateTime FirstVisitTimeStamp { get; set; }
    public DateTime LastVisitTimeStamp { get; set; }
    public DateTime FirstVisitTodayTimeStamp { get; set; }
    public DateTime LastVisitTodayTimeStamp { get; set; }
}