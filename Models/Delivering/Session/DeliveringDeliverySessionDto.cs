using Services.Helper.Enums;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.Delivering.Employee;
using Services.Models.Delivering.Session;
using Services.Models.Delivering.Station;
using Services.Models.Delivering.Vehicle;

namespace Services.Models.Delivery.Session;

public class DeliveringDeliverySessionDto
{
    public string Code { get; set; }
    public string? SessionType { get; set; }
    public string? Transit { get; set; } = null;
    public string? ParentCode { get; set; }
    public string? DriverCode { get; set; }
    public string? CoordinatorCode { get; set; }
    public string? VehicleCode { get; set; }
    public string? StartStationCode { get; set; }
    public string? EndStationCode { get; set; }
    public string? SessionGroupCode { get; set; }
    public bool? ToCustomer { get; set; }
    public string Status { get; set; }
    public string? Note { get; set; }
    public string? Excepted { get; set; }
    public string? Evidence { get; set; }
    public string? ReasonCancel { get; set; }
    public string? ReasonReject { get; set; }
    public int? TotalReceivedItems { get; set; }
    public int TotalDOs { get; set; } = 0;
    public int TotalDPs { get; set; } = 0;
    public int TotalSOs { get; set; } = 0;
    public DeliveringVehicleDto Vehicle { get; set; }
    public DeliveringEmployeeDto Employee { get; set; }
    public DeliveringStationDto? StartStation { get; set; }
    public DeliveringStationDto? EndStation { get; set; }
    public List<DeliveringDeliveryOrderDto> DeliveryOrders { get; set; }
    public List<DeliveringSessionLineDto>? DeliverySessionLines { get; set; } = new List<DeliveringSessionLineDto>();
    
    public void RandomSessionCode()
    {
        Code = "DS" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }
    
    public DeliveringDeliverySessionDto CreateSession(DeliveringDeliverySessionDto? parent = null, string? groupCode = null)
    {
        RandomSessionCode();

        if (parent != null)
        {
            ParentCode = parent.Code;
        }

        if (groupCode != null)
        {
            SessionGroupCode = groupCode;
        }

        if (DeliveryOrders?.Count > 0)
        {
            var deliverySessionLines = new List<DeliveringSessionLineDto>();
            DeliveryOrders?.ForEach(deliveryOrder =>
            {
                deliveryOrder.SessionCode = Code;

                deliveryOrder?.DeliveryOrderLines?.ForEach(dol =>
                {
                    var dsl = new DeliveringSessionLineDto();
                    dsl.CreateSessionLine(this);
                    dsl.CreateSessionLineFromOrder(deliveryOrder);
                    dsl.CreateSessionLineFromOrderLine(dol);
                    deliverySessionLines.Add(dsl);
                });
            });
            DeliverySessionLines = deliverySessionLines;
        }
        else
        {
            DeliverySessionLines?.ForEach(line => line.CreateSessionLine(this));
        }

        Status = SessionStatusEnum.New.ToString();

        return this;
    }
    
    public DeliveringDeliverySessionDto CreateDropOffSession()
    {
        if (SessionType == SessionTypeEnum.Pickup.ToString() && Status == SessionStatusEnum.Confirmed.ToString())
        {
            var dropoffSessionDto = new DeliveringDeliverySessionDto();
            dropoffSessionDto.CreateSession(this);
            dropoffSessionDto.DriverCode = DriverCode;
            dropoffSessionDto.VehicleCode = VehicleCode;
            dropoffSessionDto.SessionType = SessionTypeEnum.Dropoff.ToString();

            if (DeliverySessionLines != null && DeliverySessionLines.Count > 0)
            {
                var dropoffSessionLines = new List<DeliveringSessionLineDto>();
                DeliverySessionLines.ForEach(x =>
                {
                    var line = x.Clone();
                    line.DeliverySessionCode = dropoffSessionDto.Code;
                    dropoffSessionLines.Add(line);
                });
                dropoffSessionDto.DeliverySessionLines = dropoffSessionLines;
            }

            return dropoffSessionDto;
        }

        throw new Exception("Cannot create dropoff session");
    }
}