using System.ComponentModel.DataAnnotations;
using Services.Models.Base;
using Services.Models.Vehicle;

namespace Services.Models.VehicleType;

public class VehicleTypeDto : TrackableModel
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public float? Length { get; set; }
    public float? Height { get; set; }
    public float? Width { get; set; }
    public float? MaximumPayload { get; set; }
    public float? MaximumCapacity { get; set; }
    public string Status { get; set; } = "Active";
    public List<VehicleDto>? Vehicles { get; set; }
}