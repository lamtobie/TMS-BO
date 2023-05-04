using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrder;

public class DeliveryOrderDuplicatedCodeException : ApiException
{
    public DeliveryOrderDuplicatedCodeException() : base()
    {
        ErrorCode = "Delivery_Order_Duplicated_Code";
        ErrorMessages = new List<string>()
        {
            "Mã đơn hàng đã tồn tại"
        };
    }
}