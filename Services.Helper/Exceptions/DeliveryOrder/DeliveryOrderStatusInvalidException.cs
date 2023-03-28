using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrder;

public class DeliveryOrderStatusInvalidException : ApiException
{
    public DeliveryOrderStatusInvalidException() : base()
    {
        ErrorCode = "DELIVERY_ORDER_STATUS_INVALID";
        ErrorMessages = new List<string>()
        {
            "Trạng thái đơn hàng không hợp lệ"
        };
    }
}