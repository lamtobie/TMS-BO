using Services.Models.Base;
using Services.Models.DeliverySessionLine;
using Services.Models.Employee;
using Services.Models.Station;
using Services.Models.Vehicle;

namespace Services.Models.DeliverySession;

public class DeliverySessionTreeResultDto : TrackableModel
{
    public string? Code { get; set; }
    public string? SessionType { get; set; }
    public string? ParentCode { get; set; }
    public string? DriverCode { get; set; }
    public string? CoordinatorCode { get; set; }
    public string? VehicleCode { get; set; }
    public string? StartStationCode { get; set; }
    public string? EndStationCode { get; set; }
    public string? AVerifySourceBy { get; set; }
    public long? AVerifySourceAt { get; set; }
    public string? BVerifySourceBy { get; set; }
    public long? BVerifySourceAt { get; set; }
    public string? AVerifyDestBy { get; set; }
    public long? AVerifyDestAt { get; set; }
    public string? BVerifyDestBy { get; set; }
    public long? BVerifyDestAt { get; set; }
    public bool? ToCustomer { get; set; }
    public string? Status { get; set; }
    public string? Note { get; set; }
    public string? Excepted { get; set; }
    public string? ReasonCancel { get; set; }
    public string? ReasonReject { get; set; }
    public int? TotalReceivedItems { get; set; }

    public VehicleDto? Vehicle { get; set; }
    public EmployeeDto? Driver { get; set; }
    public EmployeeDto? Coordinator { get; set; }
    public StationDto? StartStation { get; set; }
    public StationDto? EndStation { get; set; }
    public List<DeliverySessionDto>? Childrens { get; set; } = new List<DeliverySessionDto>();
    public List<DeliverySessionLineDto>? DeliverySessionLines { get; set; } = new List<DeliverySessionLineDto>();

    public List<DeliverySessionDto>? Children { get; set; }

    public int TotalDOs
    {
        get
        {
            if (Childrens != null && Childrens.Count > 0)
            {
                return Childrens.Select(x => x.TotalDOs).Sum();
            }

            if (DeliverySessionLines != null && DeliverySessionLines.Count > 0)
            {
                return DeliverySessionLines.GroupBy(x => x.DeliveryOrderCode).Count();
            }

            return 0;
        }
    }
    public int TotalSOs
    {
        get
        {
            if (Childrens != null && Childrens.Count > 0)
            {
                return Childrens.Select(x => x.TotalSOs).Sum();
            }

            if (DeliverySessionLines != null && DeliverySessionLines.Count > 0)
            {
                return DeliverySessionLines.Where(x => x.ReferenceCode != null).GroupBy(x => x.ReferenceCode).Count();
            }

            return 0;
        }
    }
    public int TotalDPs
    {
        get
        {
            if (Childrens != null && Childrens.Count > 0)
            {
                return Childrens.Select(x => x.TotalDPs).Sum();
            }

            if (DeliverySessionLines != null && DeliverySessionLines.Count > 0)
            {
                return DeliverySessionLines.GroupBy(x => x.DeliveryPackageCode).Count();
            }

            return 0;
        }
    }

    public List<DeliverySessionLineDto>? AllDeliverySessionLines
    {
        get
        {
            if (Childrens != null && Childrens.Count > 0)
            {
                return Childrens.Where(x => x.DeliverySessionLines != null).SelectMany(x => x.DeliverySessionLines).ToList();
            }

            return DeliverySessionLines;
        }
    }
}