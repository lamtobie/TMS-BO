using Exceptions;

namespace Services.Helper.Exceptions.Employee;

public class EmployeeCoordinatorCodeNotFoundException : ApiException
{
    public EmployeeCoordinatorCodeNotFoundException() : base()
    {
        ErrorCode = "EMPLOYEE_COORDINATOR_CODE_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Mã điều phối không tìm thấy"
        };
    }
}