using Services.Models.Pagination;

namespace Services.Models.DeliveryPackageGroup;

public class DeliveryPackageGroupQuery : PaginationQuery
{
    public string? Code { get; set; }
    public string? CreatedAt { get; set; }
    public string? ConsumedAt { get; set; }
    public string? Status { get; set; }
}