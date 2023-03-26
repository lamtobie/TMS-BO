using Databases;

namespace Databases.Entities
{
    public class Station 
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string? ContactPersonAnother { get; set; }
        public string? ContactEmailAnother { get; set; }
        public string? ContactPhoneAnother { get; set; }
        public string Address { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<DeliveryRoute> StartDeliveryRoutes { get; set; }
        public virtual ICollection<DeliveryRoute> EndDeliveryRoutes { get; set; }
        public virtual ICollection<DeliveryRouteSegment> StartDeliveryRouteSegments { get; set; }
        public virtual ICollection<DeliveryRouteSegment> EndDeliveryRouteSegments { get; set; }
    }
}