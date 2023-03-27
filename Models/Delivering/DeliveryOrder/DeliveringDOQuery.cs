using Services.Models.Base;

namespace Services.Models.Delivering.DeliveryOrder;

public class DeliveringDOQuery : QueryableModel
{
    public string? DeliveryAddress { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public List<string> DeliveryServices { get; set; } = new List<string>();
    public List<string> DeliveryStatus { get; set; } = new List<string>();
}

