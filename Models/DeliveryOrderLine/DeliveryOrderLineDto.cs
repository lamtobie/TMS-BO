using Services.Models.DeliveryPackage;
using System.ComponentModel.DataAnnotations;

namespace Services.Models.DeliveryOrderLine;

public class DeliveryOrderLineDto
{
    public Guid? Id { get; set; }
    public string? DeliveryOrderCode { get; set; }
    public string? DeliveryPackageCode { get; set; }
    public string? Status { get; set; }
    [Range(1,999999)]
    public int? Quantity { get; set; }
    [Range(1,99999)]
    public int? Weight { get; set; }
    [Range(1,999)]
    public int? Length { get; set; }
    [Range(1,999)]
    public int? Width { get; set; }
    [Range(1,999)]
    public int? Height { get; set; }

    public DeliveryPackageDto? DeliveryPackage { get; set; }
    public string? Code { get; set; }
    public string? PackageStatus { get; set; }
    public string? DeliveryPackageGroupCode { get; set; }
    public string? ExternalCode { get; set; }
    public string? Name { get; set; }
    public string? Uom { get; set; }
}