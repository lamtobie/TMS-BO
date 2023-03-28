using Exceptions;

namespace Services.Helper.Exceptions.Employee;

public class EmployeeDuplicatedException : ApiException
{
    public EmployeeDuplicatedException() : base()
    {
        ErrorCode = "EMPLOYEE_DUPLICATED";
        ErrorMessages = new List<string>()
        {
            "Nhân viên đã tồn tại"
        };
    }
}