using Services.Models.DataAttribute;
using Services.Models.DeliveryOrderLine;

namespace Services.Models.DeliveryOrder;

public class DeliveryOrderDto
{
    public string? Code { get; set; }
    public bool? IsToCustomer { get; set; }
    public string? Email { get; set; }
    public string? Status { get; set; }
    public string? ParentCode { get; set; }
    public string? DriverCode { get; set; }
    public string? CoordinatorCode { get; set; }
    public string? GroupCode { get; set; }
    public string? SessionCode { get; set; }
    public int? DeliveryRouteSegmentId { get; set; }
    public string? SourceBy { get; set; }
    public long? ExpectedStartTime { get; set; }
    public long? ExpectedArrivalTime { get; set; }
    public long? ExpectedTimeConsumed { get; set; }
    public long? ActualStartTime { get; set; }
    public long? ActualArrivalTime { get; set; }
    public long? ActualTimeConsumed { get; set; }
    public string? ReferenceCode { get; set; }
    public string? ThreePLTeam { get; set; }
    public string? ProductType { get; set; }
    public float? Amount { get; set; }
    public int? Weight { get; set; }
    public bool? CodAllowed { get; set; }
    public float? CodAmount { get; set; }
    public string? CodMethod { get; set; }
    public string? StartAddress { get; set; }
    public string? StartContactPerson { get; set; }
    public string? StartContactPhone { get; set; }
    public string? StartNote { get; set; }
    public string? EndAddress { get; set; }
    public string? EndContactPerson { get; set; }
    public string? EndContactPhone { get; set; }
    public string? EndNote { get; set; }
    public DataAttributeDto[]? Additional { get; set; }
    public long? CreatedAt { get; set; }
    public long? UpdatedAt { get; set; }
    public long? CreatedBy { get; set; } = 0;
    public long? UpdatedBy { get; set; }

    public List<DeliveryOrderDto>? Childrens { get; set; } = new List<DeliveryOrderDto>();
    public List<DeliveryOrderLineDto>? DeliveryOrderLines { get; set; } = new List<DeliveryOrderLineDto>();
}