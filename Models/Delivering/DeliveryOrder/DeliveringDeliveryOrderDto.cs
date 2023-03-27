namespace Services.Models.Delivering.DeliveryOrder;

public class DeliveringDeliveryOrderDto
{
    public string Code { get; set; }
    public bool? IsToCustomer { get; set; }
    public string? Email { get; set; }
    public string Status { get; set; }
    public int? TransitOrder { get; set; }
    public int? NumberOfTransit { get; set; }
    public string? ParentCode { get; set; }
    public string? DriverCode { get; set; }
    public string? CoordinatorCode { get; set; }
    public string? GroupCode { get; set; }
    public string? SessionCode { get; set; }
    public int? DeliveryRouteSegmentId { get; set; }
    public string? SourceBy { get; set; } = "TMS Client";
    public long? ExpectedStartTime { get; set; }
    public long? ExpectedArrivalTime { get; set; }
    public long? ExpectedTimeConsumed { get; set; }
    public long? ActualStartTime { get; set; }
    public long? ActualArrivalTime { get; set; }
    public long? ActualTimeConsumed { get; set; }
    public string? ReferenceCode { get; set; }
    public string? ThreePLTeam { get; set; }
    public string? ProductType { get; set; }
    public float? TotalItems { get; set; }
    public int? Weight { get; set; }
    public bool? CodAllowed { get; set; }
    public float? CodAmount { get; set; }
    public string? CodMethod { get; set; }
    public string? StartStationCode { get; set; }
    public string? StartAddress { get; set; }
    public string? StartContactPerson { get; set; }
    public string? StartContactPhone { get; set; }
    public string? StartNote { get; set; }
    public string? EndStationCode { get; set; }
    public string? EndAddress { get; set; }
    public string? EndContactPerson { get; set; }
    public string? EndContactPhone { get; set; }
    public string? EndNote { get; set; }
    public float? Volume { get; set; } = 1000;
    public float? Distance { get; set; } = 1000;
    public string? Note { get; set; }
    public string? Reason { get; set; }
    public string? Evidence { get; set; }
    public string? ReturnAddress { get; set; }
    public bool? CODReceived { get; set; }
    public List<DeliveringDOLineDto>? DeliveryOrderLines { get; set; } = new List<DeliveringDOLineDto>();

    public int NumberOfDeliveryPackage { get; set; } = 0;

    
    
}
