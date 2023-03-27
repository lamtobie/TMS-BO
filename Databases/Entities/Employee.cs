
namespace Databases.Entities
{
    public class Employee : AggregateRoot<string>
    {
        public string Code { get; set; }
        public string EmployeeType { get; set; }
        public string FullName { get; set; }
        public string MobilePhone { get; set; }
        public string? StationCode { get; set; }
        public bool? IsStationAdmin { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? AddressId { get; set; }
        public string? ThreePLTeam { get; set; }
        public string? AvatarPicture { get; set; }
        public string? DrivingLicensePicture { get; set; }
        public string? IdentityNumberPicture { get; set; }
        public string Status { get; set; } = "Active";

        public virtual Address? Address { get; set; }
        public virtual Station Station { get; set; }
        public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; }
        public virtual ICollection<DeliveryRoute> DeliveryRoutes { get; set; }
        public virtual ICollection<DeliveryRouteSegment> DeliveryRouteSegments { get; set; }
        public virtual ICollection<DeliverySession> DriverDeliverySessions { get; set; }
        public virtual ICollection<DeliverySession> CoordinatorDeliverySessions { get; set; }
    }
}