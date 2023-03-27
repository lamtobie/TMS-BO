using Services.Models.Delivering.VehicleType;

namespace Services.Models.Delivering.Vehicle;

public class DeliveringVehicleDto
{
    public string Code { get; set; }
    public string NumberPlate { get; set; }
    public string Status { get; set; } = "Active";
    public string VehicleTypeCode { get; set; }
    public DeliveringVehicleTypeDto VehicleType { get; set; }
}