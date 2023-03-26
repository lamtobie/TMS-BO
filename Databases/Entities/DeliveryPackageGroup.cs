using Databases;

namespace Databases.Entities
{
    public class DeliveryPackageGroup
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string? ParentCode { get; set; }
        public string? DeliveryOrderCode { get; set; }

        public virtual DeliveryPackageGroup Parent { get; set; }
        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public virtual ICollection<DeliveryPackageGroup> Childrens { get; set; }
        public virtual ICollection<DeliveryPackage> DeliveryPackages { get; set; }
    }
}