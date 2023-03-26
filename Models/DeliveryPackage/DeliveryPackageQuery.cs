using Services.Models.Pagination;

namespace Services.Models.DeliveryPackage;

public class DeliveryPackageQuery : PaginationQuery
{
    public string? Code { get; set; }
    public string? CreatedAt { get; set; }
    public string? ConsumedAt { get; set; }
    public string? Status { get; set; }
}