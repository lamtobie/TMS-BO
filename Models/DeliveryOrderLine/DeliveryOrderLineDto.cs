using Services.Models.DeliveryPackage;

namespace Services.Models.DeliveryOrderLine;

public class DeliveryOrderLineDto
{
    public Guid? Id { get; set; }
    public string? DeliveryOrderCode { get; set; }
    public string? DeliveryPackageCode { get; set; }
    public string? Status { get; set; }
    public int? Quantity { get; set; }
    public int? Weight { get; set; }
    public int? Length { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }

    public DeliveryPackageDto? DeliveryPackage { get; set; }
    public string? Code { get; set; }
    public string? PackageStatus { get; set; }
    public string? DeliveryPackageGroupCode { get; set; }
    public string? ExternalCode { get; set; }
    public string? Name { get; set; }
    public string? Uom { get; set; }
}