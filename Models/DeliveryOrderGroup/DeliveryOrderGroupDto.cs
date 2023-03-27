using Services.Helper.Enums;
using Services.Models.Base;
using Services.Models.DeliveryOrder;

namespace Services.Models.DeliveryOrderGroup;

public class DeliveryOrderGroupDto : TrackableModel
{
    public string? Code { get; set; }
    public string Status { get; set; } = DeliveryOrderGroupStatusEnum.New.ToString();
    public string CancelReason { get; set; }
    public List<DeliveryOrderDto> DeliveryOrders { get; set; } = new List<DeliveryOrderDto>();
    public float TotalDOs 
    {
        get
        {
            if (DeliveryOrders.Count > 0)
            {
                var total = DeliveryOrders.Select(x => 1 + GetTotalDOs(x)).Sum();
                return total;
            }
            return 0;
        }
    }

    public float TotalDPs 
    {
        get
        {
            if (DeliveryOrders.Count > 0)
            {
                var total = DeliveryOrders.Select(x => GetTotalDPs(x)).Sum();
                return total;
            }
            return 0;
        }
    }

    public float TotalSOs 
    {
        get
        { 
            if (DeliveryOrders.Count > 0)
            {
                var total = DeliveryOrders.Select(x => 1 + GetTotalSOs(x)).Sum();
                return total;
            }
            return 0;
        }
    }
    
    public float GetTotalDOs(DeliveryOrderDto dto)
    {
        if (dto.Childrens == null || dto.Childrens.Count == 0)
        {
            return 0;
        }
        return dto.Childrens.Count + dto.Childrens.Select(x => GetTotalDOs(x)).Sum();
    }
    
    public float GetTotalSOs(DeliveryOrderDto dto)
    {
        if (dto.Childrens == null || dto.Childrens.Count == 0)
        {
            return 0;
        }
        return dto.Childrens.Count + dto.Childrens.Select(x => GetTotalDOs(x)).Sum();
    }

    public float GetTotalDPs(DeliveryOrderDto dto)
    {
        var totalLines = 0;
        if (dto.DeliveryOrderLines != null && dto.DeliveryOrderLines.Count > 0)
        {
            totalLines = dto.DeliveryOrderLines.Count;
        }
        
        if (dto.Childrens == null || dto.Childrens.Count == 0)
        {
            return totalLines;
        }
        
        return totalLines + dto.Childrens.Select(x => GetTotalDPs(x)).Sum();
    }
}