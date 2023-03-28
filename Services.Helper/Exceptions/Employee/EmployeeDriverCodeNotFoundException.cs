using Exceptions;

namespace Services.Helper.Exceptions.Employee;

public class EmployeeDriverCodeNotFoundException : ApiException
{
    public EmployeeDriverCodeNotFoundException() : base()
    {
        ErrorCode = "EMPLOYEE_DRIVER_CODE_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Mã tài xế không tìm thấy"
        };
    }
}