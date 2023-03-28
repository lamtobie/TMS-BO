using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrderGroup;

public class DeliveryOrderGroupCanceledException : ApiException
{
    public DeliveryOrderGroupCanceledException() : base()
    {
        ErrorCode = "DELIVERY_ORDER_CANCELED";
        ErrorMessages = new List<string>()
        {
            "Nhóm đơn đã được hủy"
        };
    }
}