namespace Databases.Entities;

public class Vehicle : AggregateRoot<string>
{
    public string Code { get; set; }
    public string NumberPlate { get; set; }
    public string Status { get; set; } = "Active";
    public string VehicleTypeCode { get; set; }
    public virtual VehicleType VehicleType { get; set; }
    public virtual ICollection<DeliverySession> DeliverySessions { get; set; }
}