using Exceptions;

namespace Services.Helper.Exceptions.DeliverySession;

public class DeliverySessionInvalidEmployeePermissionException : ApiException
{
    public DeliverySessionInvalidEmployeePermissionException() : base()
    {
        ErrorCode = "DELIVERY_SESSION_INVALID_EMPLOYEE_PERMISSION";
        ErrorMessages = new List<string>()
        {
            "Quyền nhân viên không hợp lệ"
        };
    }
}