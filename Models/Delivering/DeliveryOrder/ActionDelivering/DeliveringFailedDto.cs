namespace Services.Models.Delivering.DeliveryOrder.ActionDelivering;

public class DeliveringFailedDto : DeliveringActionDto
{
    public string Evidence { get; set; }
    public string Reason { get; set; }
}