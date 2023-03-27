using Services.Models.Pagination;

namespace Services.Models.DeliverySessionLine;

public class DeliverySessionLineQuery : PaginationQuery
{
    public string? Keyword { get; set; }
    public string? Status { get; set; }
}