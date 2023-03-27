using Services.Models.Base;

namespace Services.Models.DeliveryOrder;

public class DeliveryOrderQuery : QueryableModel
{
    public string? ThreePLTeam { get; set; }
    public string? DriverCode { get; set; }
    public string? CoordinatorCode { get; set; }
    public string? ExpectedArrivalTime { get; set; }
    public string? ActualTimeConsumed { get; set; }
    public string? StartStation { get; set; }
    public string? SourceBy { get; set; }
    public string? NumberOfRoute { get; set; }
}