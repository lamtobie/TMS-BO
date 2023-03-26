using Databases;

namespace Databases.Entities
{
    public class DeliveryPackage 
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string? DeliveryPackageGroupCode { get; set; }
        public string? ExternalCode { get; set; }
        public string? Name { get; set; }
        public string? Uom { get; set; }

        public virtual DeliveryPackageGroup DeliveryPackageGroup { get; set; }
        public virtual ICollection<DeliveryOrderLine> DeliveryOrderLines { get; set; }
    }
}