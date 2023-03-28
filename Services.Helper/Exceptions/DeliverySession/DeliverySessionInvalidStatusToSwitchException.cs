using Exceptions;

namespace Services.Helper.Exceptions.DeliverySession;

public class DeliverySessionInvalidStatusToSwitchException : ApiException
{
    public DeliverySessionInvalidStatusToSwitchException() : base()
    {
        ErrorCode = "DELIVERY_SESSION_INVALID_STATUS_TO_SWITCH";
        ErrorMessages = new List<string>()
        {
            "Trạng thái phiên không hợp lệ để cập nhật"
        };
    }
}