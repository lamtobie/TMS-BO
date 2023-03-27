
namespace Databases.Entities;

public class DeliveryOrderGroup : AggregateRoot<string>
{
    public string Code { get; set; }
    public string Status { get; set; }
    public string? CancelReason { get; set; }
    public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; }
}