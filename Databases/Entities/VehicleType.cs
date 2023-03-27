
namespace Databases.Entities;

public class VehicleType : AggregateRoot<string>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public float? Length { get; set; }
    public float? Height { get; set; }
    public float? Width { get; set; }
    public float? MaximumPayload { get; set; }
    public float? MaximumCapacity { get; set; }
    public string Status { get; set; } = "Active";
    public ICollection<Vehicle> Vehicles { get; set; }
}