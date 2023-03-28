using Exceptions;

namespace Services.Helper.Exceptions.Employee;

public class EmployeeDuplicatedPhoneException : ApiException
{
    public EmployeeDuplicatedPhoneException() : base()
    {
        ErrorCode = "EMPLOYEE_DUPLICATED_PHONE";
        ErrorMessages = new List<string>()
        {
            "Số điện thoại đã được sử dụng"
        };
    }
}