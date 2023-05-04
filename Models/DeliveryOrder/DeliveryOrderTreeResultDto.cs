using Services.Models.Address;
using Services.Models.DeliveryOrderLine;
using Services.Models.Employee;
using Databases.Entities;

namespace Services.Models.DeliveryOrder;

public class DeliveryOrderTreeResultDto
{
    public string? Code { get; set; }
    public string? Status { get; set; }
    public float? TotalItems { get; set; }
    public int? NumberOfTransit { get; set; }
    public string? GroupCode { get; set; }
    public string? ParentCode { get; set; }
    public string? DriverCode { get; set; }
    public string? CoordinatorCode { get; set; }
    public string? SessionCode { get; set; }
    public string? SourceBy { get; set; }
    public long? ExpectedStartTime { get; set; }
    public long? ExpectedArrivalTime { get; set; }
    public long? ExpectedTimeConsumed { get; set; }
    public long? ActualStartTime { get; set; }
    public long? ActualArrivalTime { get; set; }
    public long? ActualTimeConsumed { get; set; }
    public string? ReferenceCode { get; set; }
    public string? ThreePLTeam { get; set; }
    // public float? Amount { get; set; }
    public float? CodAmount { get; set; }
    public AddressDto? StartAddress { get; set; }
    public string? StartStationCode { get; set; }
    public string? StartContactPerson { get; set; }
    public string? StartContactPhone { get; set; }
    public string? StartNote { get; set; }
    public AddressDto? EndAddress { get; set; }
    public string? EndContactPerson { get; set; }
    public string? EndContactPhone { get; set; }
    public string? EndStationCode { get; set; }
    public string? EndNote { get; set; }
    public long? CreatedAt { get; set; }
    // public long? UpdatedAt { get; set; }
    // public long? CreatedBy { get; set; } = 0;
    // public long? UpdatedBy { get; set; }

    public virtual ICollection<Databases.Entities.DeliveryOrder> Childrens { get; set; }
    public EmployeeDto? Driver { get; set; }
    public EmployeeDto? Coordinator { get; set; }
    // public EmployeeDto? Session { get; set; }
}