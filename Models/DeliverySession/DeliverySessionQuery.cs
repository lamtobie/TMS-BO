using Services.Models.Base;
using Services.Models.Pagination;

namespace Services.Models.DeliverySession;

public class DeliverySessionQuery : QueryableModel
{
    public string? CoordinatorCode { get; set; }
    public string? DriverCode { get; set; }
    public string? StartStationCode { get; set; }
    public string? EndStationCode { get; set; }
    public string? UserId { get; set; }
}