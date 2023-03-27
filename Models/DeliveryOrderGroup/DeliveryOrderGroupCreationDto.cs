using Services.Helper.Enums;
using Services.Models.Base;

namespace Services.Models.DeliveryOrderGroup;

public class DeliveryOrderGroupCreationDto
{
    public string? Code { get; set; }
    public List<string> DeliveryOrderCodes { get; set; }
    public string Status { get; set; } = DeliveryOrderGroupStatusEnum.New.ToString();

    public void RandomDeliveryOrderGroupCode()
    {
        if (Code == null)
        {
            Code = "DG" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
        }
    }
}