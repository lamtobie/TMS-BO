namespace Services.Models.Delivering.DeliveryOrder.ActionDelivering;

public class DeliveringSuccessfulDto : DeliveringActionDto
{
    public bool CODReceived { get; set; }
    public string Evidence { get; set; }
}