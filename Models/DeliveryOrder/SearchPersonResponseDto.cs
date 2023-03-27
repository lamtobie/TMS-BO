using Services.Models.Employee;

namespace Services.Models.DeliveryOrder;

public class SearchPersonResponseDto
{
    public string Phone { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
}