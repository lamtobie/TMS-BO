using Exceptions;

namespace Services.Helper.Exceptions.DeliverySession;

public class DeliverySessionPartiallyDOException : ApiException
{
    public DeliverySessionPartiallyDOException() : base()
    {
        ErrorCode = "DELIVERY_SESSION_PARTIALLY_DO";
        ErrorMessages = new List<string>()
        {
            "Đơn hàng không đầy đủ"
        };
    }
}