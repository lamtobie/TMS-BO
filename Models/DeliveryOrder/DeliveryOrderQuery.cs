using Services.Models.Pagination;

namespace Services.Models.DeliveryOrder;

public class DeliveryOrderQuery : PaginationQuery
{
    public string? Keyword { get; set; }
    public string? ThreePLTeam { get; set; }
    public string? ClientMobilePhone { get; set; }
    public string? CreatedAt { get; set; }
    public string? ConsumedAt { get; set; }
    public string? Status { get; set; }
}