using Databases.Entities;
using Services.Models.Employee;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IEmployeeServices
{
    Task<PaginatedResultDto<Employee>> GetAll(EmployeeQuery query);
    Task<EmployeeDto> GetOneByCode(string code);
    Task<EmployeeDto> Create(EmployeeCreationDto driverCreationDto);
    Employee Delete(string id);
    Task<EmployeeDto> Update(string driverCode, EmployeeDto driver);
}