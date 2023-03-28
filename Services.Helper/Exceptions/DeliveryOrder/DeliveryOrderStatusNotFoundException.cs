using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrder;

public class DeliveryOrderStatusNotFoundException : NotFoundException
{
    public DeliveryOrderStatusNotFoundException() : base()
    {
        ErrorCode = "DELIVERY_ORDER_STATUS_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Trạng thái đơn không tìm thấy"
        }; 
    }
}