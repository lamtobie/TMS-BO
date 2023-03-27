using Services.Models.Base;

namespace Services.Models.Vehicle;

public class VehicleQuery : QueryableModel
{
    public string? VehicleTypeCode { get; set; }
}