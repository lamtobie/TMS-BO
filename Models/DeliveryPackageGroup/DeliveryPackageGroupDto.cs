namespace Services.Models.DeliveryPackageGroup;

public class DeliveryPackageGroupDto
{
    public string Code { get; set; }
    public string Status { get; set; }
    public string? ParentCode { get; set; }
    public string DeliveryOrderCode { get; set; }
}