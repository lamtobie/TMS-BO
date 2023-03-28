using Exceptions;

namespace Services.Helper.Exceptions.DeliverySession;

public class DeliverySessionNotFoundException : ApiException
{
    public DeliverySessionNotFoundException() : base()
    {
        ErrorCode = "DELIVERY_SESSION_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Mã phiên không tìm thấy"
        };
    }
}