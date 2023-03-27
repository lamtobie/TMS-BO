
namespace Databases.Entities
{
    public class DeliverySession : AggregateRoot<string>
    {
        public string Code { get; set; }
        public string? SessionType { get; set; }
        public string? ParentCode { get; set; }
        public string? DriverCode { get; set; }
        public string? CoordinatorCode { get; set; }
        public string? VehicleCode { get; set; }
        public string? StartStationCode { get; set; }
        public string? EndStationCode { get; set; }
        public string? SessionGroupCode { get; set; }
        public bool? ToCustomer { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public string? Evidence { get; set; }
        public string? Excepted { get; set; }
        public string? ReasonCancel { get; set; }
        public string? ReasonReject { get; set; }
        public int? TotalReceivedItems { get; set; }

        public virtual DeliverySession Parent { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Employee Driver { get; set; }
        public virtual Employee Coordinator { get; set; }
        public virtual Station StartStation { get; set; }
        public virtual Station EndStation { get; set; }
        public virtual DeliverySessionGroup SessionGroup { get; set; }
        public virtual ICollection<DeliverySession> Childrens { get; set; }
        public virtual ICollection<DeliverySessionLine> DeliverySessionLines { get; set; }
        public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; }
    }
}