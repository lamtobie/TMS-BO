using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Employee;
using Services.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(
          //  IAuthorizedServices authorizedServices,
            IEmployeeServices employeeServices
            ) : base()
        {
            _employeeServices = employeeServices;
        }

        [HttpGet("[action]")]
        public async Task<PaginatedResultDto<Employee>> GetAll([FromQuery] EmployeeQuery query)
        {
            var result = await _employeeServices.GetAll(query);
            return result;
        }

        [HttpGet("[action]/{employeeCode}")]
        public async Task<BaseModel<EmployeeDto>> GetOne([FromRoute] string employeeCode)
        {
            var result = await _employeeServices.GetOneByCode(employeeCode);
            var response = new BaseModel<EmployeeDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpPost("[action]")]
        public async Task<BaseModel<EmployeeDto>> Create([FromBody] EmployeeCreationDto employee)
        {
            var result = await _employeeServices.Create(employee);
            var response = new BaseModel<EmployeeDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpDelete("[action]/{employeeCode}")]
        public async Task<BaseModel<Employee>> Delete([FromRoute] string employeeCode)
        {
            var result = _employeeServices.Delete(employeeCode);
            var response = new BaseModel<Employee>()
            {
                Data = result
            };
            return response;
        }

        [HttpPatch("[action]/{employeeCode}")]
        public async Task<BaseModel<EmployeeDto>> Update([FromBody] EmployeeDto employee, [FromRoute] string employeeCode)
        {
            var result = await _employeeServices.Update(employeeCode, employee);
            var response = new BaseModel<EmployeeDto>()
            {
                Data = result
            };
            return response;
        }
    }
}