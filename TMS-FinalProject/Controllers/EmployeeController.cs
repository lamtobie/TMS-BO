using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Employee;
using Services.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGet("GetDrivers")]
        public async Task<PaginatedResultDto<Employee>> GetDrivers([FromQuery] EmployeeQuery query)
        {
            var result = await _employeeServices.GetDrivers(query);
            return result;
        }
        [HttpGet("GetCoordinators")]
        public async Task<PaginatedResultDto<Employee>> GetCoordinators([FromQuery] EmployeeQuery query)
        {
            var result = await _employeeServices.GetCoordinators(query);
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

        [HttpPost("UploadAvatar/{code}")]
        public async Task<int> UploadAvatarPicture(string code)
        {
            var files = Request.Form.Files;
            var result = await _employeeServices.AddAvatarPicture(files,code);
            return result;
        }
        [HttpPost("UploadIdentity/{code}")]
        public async Task<int> UploadIdentityPicture(string code)
        {
            var files = Request.Form.Files;
            var result = await _employeeServices.AddIdentityPicture(files, code);
            return result;
        }
        [HttpPost("UploadLicense/{code}")]
        public async Task<int> UploadLicensePicture(string code)
        {
            var files = Request.Form.Files;
            var result = await _employeeServices.AddLicensePicture(files, code);
            return result;
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

        [HttpGet("GetAvatarImage/{name}")]
        public IActionResult GetPhotos(string name)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Avatar");
                var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var photos = Directory.EnumerateFiles(pathToRead)
                    .Where(a => a.Contains(name))
                    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)));
                return Ok(new { photos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetIdentityImage/{name}")]
        public IActionResult GetIdentityPhotos(string name)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Identity");
                var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var photos = Directory.EnumerateFiles(pathToRead)
                    .Where(a => a.Contains(name))
                    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)));
                return Ok(new { photos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("GetLicenseImage/{name}")]
        public IActionResult GetLicensePhotos(string name)
        {
            try
            {
                var folderName = Path.Combine("Resources", "License");
                var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var photos = Directory.EnumerateFiles(pathToRead)
                    .Where(a => a.Contains(name))
                    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)));
                return Ok(new { photos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}