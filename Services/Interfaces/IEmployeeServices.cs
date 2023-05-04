using Databases.Entities;
using Microsoft.AspNetCore.Http;
using Services.Models.Employee;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IEmployeeServices
{
    Task<PaginatedResultDto<Employee>> GetAll(EmployeeQuery query);
    Task<PaginatedResultDto<Employee>> GetDrivers(EmployeeQuery query);
    Task<PaginatedResultDto<Employee>> GetCoordinators(EmployeeQuery query);
    Task<EmployeeDto> GetOneByCode(string code);
    Task<EmployeeDto> Create(EmployeeCreationDto driverCreationDto);
    Employee Delete(string id);
    Task<EmployeeDto> Update(string driverCode, EmployeeDto driver);
    Task<int> AddAvatarPicture(IFormFileCollection items,string code);
    Task<int> AddIdentityPicture(IFormFileCollection items, string code);
    Task<int> AddLicensePicture(IFormFileCollection items, string code);

}