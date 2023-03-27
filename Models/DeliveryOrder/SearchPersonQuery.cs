using Services.Models.Base;

namespace Services.Models.DeliveryOrder;

public class SearchPersonQuery : QueryableModel
{
    public string Phone { get; set; }
    public string? FullName { get; set; }
    // public int? PageSize { get; set; } = 100;
}