using Exceptions;

namespace Services.Helper.Exceptions.DeliverySession;

public class DeliverySessionDOInAnotherException : ApiException
{
    public DeliverySessionDOInAnotherException() : base()
    {
        ErrorCode = "DELIVERY_SESSION_EXIST_DO_IN_ANOTHER";
        ErrorMessages = new List<string>()
        {
            "Tồn tại đơn hàng trong một phiên khác"
        };
    }

    public DeliverySessionDOInAnotherException(List<string> doCodes) : base()
    {
        ErrorCode = "DELIVERY_SESSION_EXIST_DO_IN_ANOTHER";
        ErrorMessages = new List<string>()
        {
            $"Đơn hàng {string.Join(", ", doCodes)} đã thuộc phiên bàn giao khác"
        };
    }
}