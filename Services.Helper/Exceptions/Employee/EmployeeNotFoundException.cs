
namespace Services.Helper.Exceptions.Employee;

public class EmployeeNotFoundException : ApiException
{
    public EmployeeNotFoundException() : base()
    {
        ErrorCode = "EMPLOYEE_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Nhân viên không tìm thấy"
        };
    }
}