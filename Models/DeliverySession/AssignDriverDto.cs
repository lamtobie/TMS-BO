namespace Services.Models.DeliverySession;

public class AssignDriverDto
{
    public string DriverCode { get; set; }
    public string? SessionCode { get; set; }
    public string? StationCode { get; set; }

    public List<string> DeliveryOrderCodes { get; set; }
    public string VehicleCode { get; set; }
}