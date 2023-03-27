using Services.Models.DeliverySession;

namespace Services.Models.DeliverySessionGroup;

public class DeliverySessionGroupDto
{
    public string Code { get; set; }
    public List<DeliverySessionDto>? Sessions { get; set; }

    public void RandomSessionGroupCode()
    {
        Code = "DSG" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }
}