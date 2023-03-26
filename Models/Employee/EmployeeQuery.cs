using Services.Models.Base;

namespace Services.Models.Employee;

public class EmployeeQuery : QueryableModel
{
    public string? StationCode { get; set; }
    public string? ThreePLTeam { get; set; }
    public string? EmployeeType { get; set; }
}