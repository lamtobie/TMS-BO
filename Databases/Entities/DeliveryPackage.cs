
namespace Databases.Entities
{
    public class DeliveryPackage : AggregateRoot<string>
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string? ExternalCode { get; set; }
        public string? ExternalSOCode { get; set; }
        public string? Name { get; set; }
        public string? Uom { get; set; }

        public virtual ICollection<DeliveryOrderLine> DeliveryOrderLines { get; set; }
    }
}