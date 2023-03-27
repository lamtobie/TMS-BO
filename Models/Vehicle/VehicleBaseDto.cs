namespace Services.Models.Vehicle;

public class VehicleBaseDto
{
    public string? Code { get; set; }
    public string? NumberPlate { get; set; }
    public string? Status { get; set; } = "Active";
    public string? VehicleTypeCode { get; set; }
    public long CreatedAt { get; set; }

    public long? UpdatedAt { get; set; }

    public long? CreatedBy { get; set; } = 0;

    public long? UpdatedBy { get; set; }
}