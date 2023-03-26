
namespace Services.Helper.Exceptions.Employee;

public class EmployeeDuplicatedEmailException : ApiException
{
    public EmployeeDuplicatedEmailException() : base()
    {
        ErrorCode = "EMPLOYEE_DUPLICATED_EMAIL";
        ErrorMessages = new List<string>()
        {
            "Email đã được sử dụng"
        };
    }
}