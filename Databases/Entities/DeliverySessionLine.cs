
namespace Databases.Entities
{
    public class DeliverySessionLine : AggregateRoot<Guid>
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

        public virtual DeliverySession DeliverySession { get; set; }
    }
}