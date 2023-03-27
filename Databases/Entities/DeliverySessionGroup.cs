
namespace Databases.Entities
{
    public class DeliverySessionGroup : AggregateRoot<string>
    {
        public string Code { get; set; }

        public virtual ICollection<DeliverySession> Sessions { get; set; }
    }
}