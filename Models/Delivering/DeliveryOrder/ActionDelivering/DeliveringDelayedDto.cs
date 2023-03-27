namespace Services.Models.Delivering.DeliveryOrder.ActionDelivering;

public class DeliveringDelayedDto : DeliveringActionDto
{
    public long ExpectedArrivalTime { get; set; }
    public string Reason { get; set; }
    public string? Note { get; set; }
}