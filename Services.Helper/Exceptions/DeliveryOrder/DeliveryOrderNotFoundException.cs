using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrder;

public class DeliveryOrderNotFoundException : ApiException
{
    public DeliveryOrderNotFoundException() : base()
    {
        ErrorCode = "DELIVERY_ORDER_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            $"Mã DO không tồn tại"
        };
    }
}