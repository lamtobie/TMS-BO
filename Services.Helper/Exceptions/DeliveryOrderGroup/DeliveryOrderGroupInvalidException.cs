using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrderGroup;

public class DeliveryOrderGroupInvalidException : ApiException
{
    public DeliveryOrderGroupInvalidException() : base()
    {
        ErrorCode = "DELIVERY_ORDER_GROUP_INVALID";
        ErrorMessages = new List<string>()
        {
            "Nhóm đơn không hợp lệ"
        };
    }
}