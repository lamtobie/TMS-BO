
namespace Databases.Entities
{
    public class DeliveryOrderLine : AggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public string DeliveryOrderCode { get; set; }
        public string DeliveryPackageCode { get; set; }
        public string Status { get; set; }
        public int? Quantity { get; set; }
        public int? Weight { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        
        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public virtual DeliveryPackage DeliveryPackage { get; set; }
    }
}