using Services.Models.Pagination;

namespace Services.Models.DeliveryRouteSegment;

public class DeliveryRouteSegmentQuery : PaginationQuery
{
    public string? DeliveryRouteId { get; set; }
    public string? DriverCode { get; set; }
    public string? CreatedBy { get; set; }
    public string? StartStationCode { get; set; }
    public string? EndStationCode { get; set; }
    public string? CreatedAt { get; set; }
    public string? ConsumedAt { get; set; }
    public string? Status { get; set; }
}