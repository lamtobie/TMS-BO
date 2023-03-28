using System.Security.Cryptography.X509Certificates;
using Exceptions;

namespace Services.Helper.Exceptions.DeliveryOrderGroup;

public class DeliveryOrderGroupNotFoundException : NotFoundException
{
    public DeliveryOrderGroupNotFoundException() : base()
    {
        ErrorCode = "DELIVERY_ORDER_GROUP_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Mã nhóm đơn không được tìm thấy"
        };
    }
}