using Repositories;
using Databases.Entities;
using Services.Models.Employee;

namespace Repositories.EmployeeRepository;

public interface IEmployeeRepositories : IRepository<Employee, string>
{
    IQueryable<Employee> GetAllEmployees(EmployeeQuery query);
    Employee? GetEmployeeByCode(string code);
    Employee Create(Employee driver);
    Employee DeleteEmployee(string code);
    Employee UpdateEmployee(Employee driver);
    Employee? GetEmployeeByPhone(string phone);
    Employee? GetEmployeeByEmail(string email);
    Employee? GetEmployeeByPhoneAndEmail(string phone, string email);
}