using Services.Helper.Extensions;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.Delivery.Session;

namespace Services.Models.Delivering.Session;

public class DeliveringSessionLineDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string DeliverySessionCode { get; set; }
    public string? DeliveryOrderGroupCode { get; set; }
    public string? DeliveryOrderParentCode { get; set; }
    public string? DeliveryOrderChildrenCode { get; set; }
    public string? DeliveryOrderCode { get; set; }
    public string? ReferenceCode { get; set; }
    public string? DeliveryPackageGroupCode { get; set; }
    public string? DeliveryPackageCode { get; set; }
    public string Status { get; set; }
    public long? ConsumedAt  { get; set; }
    public string? ConsumedBy { get; set; }
    public void RandomSessionLineCode()
    {
        Id = Guid.NewGuid();
        Code = "DSL" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }

    public DeliveringSessionLineDto CreateSessionLine(DeliveringDeliverySessionDto sessionDto)
    {
        RandomSessionLineCode();

        DeliverySessionCode = sessionDto.Code;

        return this;
    }

    public DeliveringSessionLineDto CreateSessionLineFromOrder(DeliveringDeliveryOrderDto orderDto)
    {
        DeliveryOrderGroupCode = orderDto.GroupCode;
        DeliveryOrderParentCode = orderDto.ParentCode;
        DeliveryOrderCode = orderDto.Code;
        ReferenceCode = orderDto.ReferenceCode;

        return this;
    }

    public DeliveringSessionLineDto CreateSessionLineFromOrderLine(DeliveringDOLineDto orderDeliveringDoLineDto)
    {
        DeliveryPackageCode = orderDeliveringDoLineDto.Code;

        return this;
    }
    public DeliveringSessionLineDto Clone()
    {
        var cloned = this.ShallowClone();

        cloned.RandomSessionLineCode();

        return cloned;
    }

}