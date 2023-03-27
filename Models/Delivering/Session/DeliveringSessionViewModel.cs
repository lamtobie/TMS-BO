using Services.Models.Delivery.Session;
using Services.Models.Pagination;

namespace Services.Models.Delivering.Session;

public class DeliveringSessionViewModel : PaginatedDTO<DeliveringDeliverySessionDto>
{
    public SessionSummary Summary { get; set; }
}