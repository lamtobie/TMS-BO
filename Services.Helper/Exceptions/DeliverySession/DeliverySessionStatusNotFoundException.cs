using Exceptions;

namespace Services.Helper.Exceptions.DeliverySession;

public class DeliverySessionStatusNotFoundException : NotFoundException
{
    public DeliverySessionStatusNotFoundException() : base()
    {
        ErrorCode = "DELIVERY_SESSION_STATUS_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Trạng thái phiên không tìm thấy"
        }; 
    }
}