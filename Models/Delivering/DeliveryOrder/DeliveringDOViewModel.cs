using Services.Models.Pagination;

namespace Services.Models.Delivering.DeliveryOrder;

public class DeliveringDOViewModel : PaginatedDTO<DeliveringDeliveryOrderDto>
{
    public DOSummary Summary { get; set; }
}