using Services.Models.Pagination;

namespace Services.Models.DeliveryRoute;

public class DeliveryRouteQuery : PaginationQuery
{
    public string? DriverCode { get; set; }
    public string? CreatedBy { get; set; }
    public string? StartStationCode { get; set; }
    public string? EndStationCode { get; set; }
    public string? CreatedAt { get; set; }
    public string? ConsumedAt { get; set; }
    public string? Status { get; set; }
}