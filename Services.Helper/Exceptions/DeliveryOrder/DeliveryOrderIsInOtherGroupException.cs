using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrder;

public class DeliveryOrderIsInOtherGroupException : ApiException
{
    public DeliveryOrderIsInOtherGroupException() : base()
    {
        ErrorCode = "DELIVERY_ORDER_IS_IN_OTHER_GROUP";
        ErrorMessages = new List<string>()
        {
            "Đơn đã nằm trong một nhóm khác"
        };
    }
}