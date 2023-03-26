namespace Services.Models.DeliveryRoute;

public class DeliveryRouteDto
{
    public int? Id { get; set; }
    public short Order { get; set; }
    public float Length { get; set; }
    public string Status { get; set; }
    public int? ExpectedStartTime { get; set; }
    public int? ExpectedArrivalTime { get; set; }
    public int? ExpectedTimeConsumed { get; set; }
    public int? ActualStartTime { get; set; }
    public int? ActualArrivalTime { get; set; }
    public int? ActualTimeConsumed { get; set; }
    public string StartStationId { get; set; }
    public string? EndStationId { get; set; }
    public string? DriverCode { get; set; }
}