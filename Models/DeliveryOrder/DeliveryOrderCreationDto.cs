using Services.Models.Address;
using Services.Models.DataAttribute;
using Services.Models.DeliveryOrderLine;

namespace Services.Models.DeliveryOrder;

public class PickupInfo
{
    public string? StartStationCode { get; set; }
    public AddressDto StartAddress { get; set; }
    public string StartContactPerson { get; set; }
    public string StartContactPhone { get; set; }
    public long? ExpectedStartTime { get; set; }
    public string? StartNote { get; set; }
}

public class DropoffInfo
{
    public string? EndStationCode { get; set; }
    public AddressDto EndAddress { get; set; }
    public string EndContactPerson { get; set; }
    public string EndContactPhone { get; set; }
    public string? EndNote { get; set; }
    public bool? IsToCustomer { get; set; }
    public long? ExpectedArrivalTime { get; set; }
    public long? ExpectedTimeConsumed { get; set; }
    public string? ReferenceCode { get; set; }
    public string? ThreePLTeam { get; set; }
    public string? ProductType { get; set; }
    public float? TotalItems { get; set; }
    public int? Weight { get; set; }
    public bool? CodAllowed { get; set; }
    public float? CodAmount { get; set; }
    public string? CodMethod { get; set; }
    public DataAttributeDto[]? Additional { get; set; }
    public List<DeliveryOrderLineDto>? DeliveryOrderLines { get; set; }
}

public class DeliveryOrderManyDropoffCreationDto
{
    public PickupInfo PickupInfo { get; set; }
    public List<DropoffInfo> DropoffInfo { get; set; }
}

public class DeliveryOrderInTransitDto
{
    public long? ExpectedStartTime { get; set; }
    public long? ExpectedArrivalTime { get; set; }
    public long? ExpectedTimeConsumed { get; set; }
    public string? ReferenceCode { get; set; }
    public string ThreePLTeam { get; set; }
    public string ProductType { get; set; }
    public float? TotalItems { get; set; }
    public int? Weight { get; set; }
    public bool? CodAllowed { get; set; }
    public float? CodAmount { get; set; }
    public string? CodMethod { get; set; }
    public string? StartStationCode { get; set; }
    public string StartContactPerson { get; set; }
    public string StartContactPhone { get; set; }
    public string StartNote { get; set; }
    public string? EndStationCode { get; set; }
    public string EndContactPerson { get; set; }
    public string EndContactPhone { get; set; }
    public string EndNote { get; set; }
    public Guid? StartAddressId { get; set; }
    public Guid? EndAddressId { get; set; }
    public AddressDto? StartAddress { get; set; }
    public AddressDto? EndAddress { get; set; }
    public DataAttributeDto[]? Additional { get; set; }
    public List<DeliveryOrderLineDto>? DeliveryOrderLines { get; set; }
}

public class DeliveryOrderInTransitCreationDto
{
    public DeliveryOrderInTransitDto Data { get; set; }
    public List<DeliveryOrderInTransitDto>? Subs { get; set; }
}