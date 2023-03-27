using Services.Models.DataAttribute;
using Services.Models.DeliveryOrder;
using Services.Models.DeliveryOrderLine;
using Services.Models.DeliverySession;

namespace Services.Models.DeliverySessionLine;

public class DeliverySessionLineDto
{
    public Guid? Id { get; set; }
    public string? Code { get; set; }
    public string? DeliverySessionCode { get; set; }
    public string? DeliveryOrderGroupCode { get; set; }
    public string? DeliveryOrderParentCode { get; set; }
    public string? DeliveryOrderChildrenCode { get; set; }
    public string? DeliveryOrderCode { get; set; }
    public string? ReferenceCode { get; set; }
    public string? DeliveryPackageGroupCode { get; set; }
    public string? DeliveryPackageCode { get; set; }
    public string? Status { get; set; }
    public long? ConsumedAt { get; set; }
    public string? ConsumedBy { get; set; }

    public void RandomSessionLineCode()
    {
        Id = Guid.NewGuid();
        Code = "DSL" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }

    public DeliverySessionLineDto CreateSessionLine(DeliverySessionDto sessionDto)
    {
        RandomSessionLineCode();

        DeliverySessionCode = sessionDto.Code;

        return this;
    }

    public DeliverySessionLineDto CreateSessionLineFromOrder(DeliveryOrderDto orderDto)
    {
        DeliveryOrderGroupCode = orderDto.GroupCode;
        DeliveryOrderParentCode = orderDto.ParentCode;
        DeliveryOrderCode = orderDto.Code;
        ReferenceCode = orderDto.ReferenceCode;

        return this;
    }

    public DeliverySessionLineDto CreateSessionLineFromOrderLine(DeliveryOrderLineDto orderLineDto)
    {
        DeliveryPackageCode = orderLineDto.Code;

        return this;
    }

}