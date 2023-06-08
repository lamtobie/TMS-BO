using Services.Helper.Enums;
using Services.Models.Base;
using Services.Models.DeliveryOrder;
using Services.Models.DeliverySessionGroup;
using Services.Models.DeliverySessionLine;
using Services.Models.Employee;
using Services.Models.Station;
using Services.Models.Vehicle;

namespace Services.Models.DeliverySession;

public class DeliverySessionDto : TrackableModel
{
    public string? Code { get; set; }
    public string? SessionType { get; set; }
    public string? ParentCode { get; set; }
    public string? DriverCode { get; set; }
    public string? CoordinatorCode { get; set; }
    public string? VehicleCode { get; set; }
    public string? StartStationCode { get; set; }
    public string? EndStationCode { get; set; }
    public string? SessionGroupCode { get; set; }
    public long? AVerifyAt { get; set; }
    public string? AVerifyBy { get; set; }
    public long? BVerifyAt { get; set; }
    public string? BVerifyBy { get; set; }
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
    public DeliverySessionGroupDto? SessionGroup { get; set; }
    public List<DeliverySessionDto>? Childrens { get; set; } = new List<DeliverySessionDto>();
    public List<DeliverySessionLineDto>? DeliverySessionLines { get; set; } = new List<DeliverySessionLineDto>();
    public List<DeliveryOrderDto>? DeliveryOrders { get; set; }


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

    public List<string> GetDOCodes()
    {
        var doCodes = new List<string>();

        if (DeliveryOrders?.Count > 0)
        {
            DeliveryOrders?.ForEach(x =>
            {
                doCodes.Add(x.Code);
            });
        }
        else
        {
            DeliverySessionLines?.ForEach(x =>
            {
                if (x.DeliveryOrderParentCode != null)
                {
                    doCodes.Add(x.DeliveryOrderParentCode);
                }

                if (x.DeliveryOrderCode != null)
                {
                    doCodes.Add(x.DeliveryOrderCode);
                }
            });
        }

        doCodes = doCodes.Distinct().ToList();

        return doCodes;
    }

    public void RandomSessionCode()
    {
        Code = "DS" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }

    public DeliverySessionDto CreateSessionLinesFromDOs(List<DeliveryOrderDto> deliveryOrderDtos)
    {
        var deliverySessionLines = new List<DeliverySessionLineDto>();
        deliveryOrderDtos.ForEach(deliveryOrder =>
        {
            deliveryOrder?.DeliveryOrderLines.ForEach(doLine =>
            {
                deliverySessionLines.Add(new DeliverySessionLineDto()
                {
                    DeliverySessionCode = Code,
                    DeliveryOrderGroupCode = deliveryOrder.GroupCode,
                    DeliveryOrderParentCode = deliveryOrder.ParentCode,
                    DeliveryOrderCode = deliveryOrder.Code,
                    ReferenceCode = deliveryOrder.ReferenceCode,
                    DeliveryPackageCode = doLine.DeliveryPackageCode,
                    DeliveryPackageGroupCode = doLine.DeliveryPackageGroupCode
                });
            });
        });

        DeliverySessionLines = deliverySessionLines;

        return this;
    }

    public DeliverySessionDto CreateSession(DeliverySessionDto? parent = null, string? groupCode = null)
    {
        RandomSessionCode();
        if (DriverCode != null)
        {
            DriverCode = this.DriverCode;
        }

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
            var deliverySessionLines = new List<DeliverySessionLineDto>();
            DeliveryOrders?.ForEach(deliveryOrder =>
            {
                deliveryOrder.SessionCode = Code;

                deliveryOrder?.DeliveryOrderLines?.ForEach(dol =>
                {
                    var dsl = new DeliverySessionLineDto();
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

    public DeliverySessionDto HandedOver(string userId, DeliverySessionConfirmDto data)
    {
        Note = data.Note;
        Status = SessionStatusEnum.HandedOver.ToString();

        return this;
    }

    public DeliverySessionDto Cancel(string? reasonCancel)
    {
        ReasonCancel = reasonCancel;
        Status = SessionStatusEnum.Cancelled.ToString();

        return this;
    }
    public DeliverySessionDto Returned(string? note)
    {
        Note = note;
        Status = SessionStatusEnum.HandOverFailed.ToString();
        SessionType = SessionTypeEnum.Refund.ToString();
        return this;
    }


    public bool AssignToDriver(EmployeeDto driver)
    {
        if (driver.EmployeeType == "driver")
        {
            DriverCode = driver.Code;
            return true;
        }
        return false;
    }

    public bool AssignToVehicle(VehicleDto vehicle)
    {
        if (vehicle.Status == "active")
        {
            VehicleCode = vehicle.Code;
            return true;
        }
        return false;
    }
    public bool AssignToStation(StationDto station)
    {
        if (station.Status == "active")
        {
            EndStationCode = station.Code;
            return true;
        }
        return false;
    }

    public void UpdateSessionCodeToDOs(string? code)
    {
        DeliveryOrders?.ForEach(x => x.SessionCode = code ?? Code);
    }

    public string ComputeDOStatus()
    {
        var doStatus = string.Empty;
        
        if (SessionType == SessionTypeEnum.Pickup.ToString() && Status == SessionStatusEnum.New.ToString())
        {
            doStatus = DeliveryOrderStatusEnum.Assigned.ToString();
        }
        else if (SessionType == SessionTypeEnum.Pickup.ToString() && Status == SessionStatusEnum.HandedOver.ToString())
        {
            doStatus = DeliveryOrderStatusEnum.Picking.ToString();
        }
        else if (SessionType == SessionTypeEnum.Refund.ToString() && Status == SessionStatusEnum.HandOverFailed.ToString())
        {
            if (doStatus.Equals(DeliveryOrderStatusEnum.DeliveredFailureAndReturning.ToString()))
                doStatus = DeliveryOrderStatusEnum.DeliveredFailureAndReturned.ToString();
            if (doStatus.Equals(DeliveryOrderStatusEnum.DeliveryDelay.ToString()))
                doStatus=DeliveryOrderStatusEnum.DeliveryDelayAndReturned.ToString();
            if (doStatus.Equals(DeliveryOrderStatusEnum.DeliveryDelayAndReturning.ToString()))
                doStatus = DeliveryOrderStatusEnum.DeliveryDelayAndReturned.ToString();
        }
        else if (SessionType == SessionTypeEnum.Pickup.ToString() && Status == SessionStatusEnum.Confirmed.ToString())
        {
            doStatus = DeliveryOrderStatusEnum.Picked.ToString();
        }
        else if (SessionType == SessionTypeEnum.Pickup.ToString() && Status == SessionStatusEnum.Cancelled.ToString())
        {
            doStatus = DeliveryOrderStatusEnum.Cancel.ToString();
            //doStatus = DeliveryOrderStatusEnum.New.ToString();
        }
        else if (SessionType == SessionTypeEnum.Dropoff.ToString() && Status == SessionStatusEnum.New.ToString())
        {
            doStatus = DeliveryOrderStatusEnum.Picked.ToString();
        }
        else if (SessionType == SessionTypeEnum.Dropoff.ToString() && Status == SessionStatusEnum.HandedOver.ToString())
        {
            if (EndStationCode != null)
            {
                doStatus = DeliveryOrderStatusEnum.DeliveringToStation.ToString();
            }
            else
            {
                doStatus = DeliveryOrderStatusEnum.DeliveringToClient.ToString();
            }
        }
        else if (SessionType == SessionTypeEnum.Dropoff.ToString() && Status == SessionStatusEnum.Confirmed.ToString())
        {
            if (EndStationCode != null)
            {
                doStatus = DeliveryOrderStatusEnum.DeliveredToStationSuccess.ToString();
            }
            else
            {
                doStatus = DeliveryOrderStatusEnum.DeliveredToClientSuccessful.ToString();
            }
        }
        else if (SessionType == SessionTypeEnum.Dropoff.ToString() && Status == SessionStatusEnum.Cancelled.ToString())
        {
            doStatus = DeliveryOrderStatusEnum.Picked.ToString();
            // if (EndStationCode != null)
            // {
            //     doStatus = DeliveryOrderStatusEnum.DeliveredToStationFailure.ToString();
            // }
            // else
            // {
            //     doStatus = DeliveryOrderStatusEnum.DeliveredToClientFailure.ToString();
            // }
        }

        return doStatus;
    }
}