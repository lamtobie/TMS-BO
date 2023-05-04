using AutoMapper;
using Databases.Entities;
using Databases.Interfaces;
using Microsoft.AspNetCore.Http;
using Repositories.EmployeeRepository;
using Repositories.StationRepository;
using Services.Helper.Exceptions.Employee;
using Services.Helper.Exceptions.Station;
using Services.Interfaces;
using Services.Models.Employee;
using Services.Models.Pagination;
using System.Reflection.Metadata;

namespace Services.Implementations;

public class EmployeeServices : IEmployeeServices
{
    private readonly ICommonServices _commonServices;

    private readonly IEmployeeRepositories _employeeRepositories;
    private readonly IStationRepositories _stationRepositories;

    // Mapper
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeServices(
        ICommonServices commonServices,
        IEmployeeRepositories employeeRepositories,
        IStationRepositories stationRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _employeeRepositories = employeeRepositories;
        _stationRepositories = stationRepositories;
        _mapper = mapper;
        _unitOfWork = employeeRepositories.UnitOfWork;
    }

    public async Task<EmployeeDto> Create(EmployeeCreationDto employeeCreationDto)
    {
        await _unitOfWork.BeginTransactionAsync();
        var station = _stationRepositories.GetStationByCode(employeeCreationDto.StationCode);
        if (station == null)
        {
            throw new StationNotFoundException()
            {
                ErrorMessages = new List<string>()
                {
                    "Station không tồn tại. Vui lòng chọn station khác hoặc tạo mới"
                }
            };
        }

        var existedEmployee = _employeeRepositories.GetEmployeeByPhone(employeeCreationDto.MobilePhone);
        if (existedEmployee != null && existedEmployee.Status == "Active")
        {
            throw new EmployeeDuplicatedPhoneException()
            {
                ErrorMessages = new List<string>()
                {
                    "Số điện thoại đã tồn tại và đang được kích hoạt"
                }
            };
        }

        if (!string.IsNullOrEmpty(employeeCreationDto.Email))
        {
            existedEmployee = _employeeRepositories.GetEmployeeByEmail(employeeCreationDto.Email);
            if (existedEmployee != null && existedEmployee.Status == "Active")
            {
                throw new EmployeeDuplicatedEmailException()
                {
                    ErrorMessages = new List<string>()
                    {
                        "Email đã tồn tại và đang được kích hoạt"
                    }
                };
            }
        }

        var employee = _mapper.Map<EmployeeCreationDto, Employee>(employeeCreationDto);
        _employeeRepositories.Create(employee);
        var employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);
        await _unitOfWork.CommitTransactionAsync();
        return employeeDto;
    }

    public async Task<PaginatedResultDto<Employee>> GetAll(EmployeeQuery query)
    {
        var queryable = _employeeRepositories.GetAllEmployees(query);
        var result = _commonServices.CreatePaginationResponse<Employee>(queryable, query);
        return result;
    }
    public async Task<PaginatedResultDto<Employee>> GetDrivers(EmployeeQuery query)
    {
        var queryable = _employeeRepositories.GetAllDrivers(query);
        var result = _commonServices.CreatePaginationResponse<Employee>(queryable, query);
        return result;
    }
    public async Task<PaginatedResultDto<Employee>> GetCoordinators(EmployeeQuery query)
    {
        var queryable = _employeeRepositories.GetAllCoordinators(query);
        var result = _commonServices.CreatePaginationResponse<Employee>(queryable, query);
        return result;
    }

    public async Task<EmployeeDto> GetOneByCode(string code)
    {
        var existedEmployee = _employeeRepositories.GetEmployeeByCode(code);
        if (existedEmployee == null)
        {
            throw new EmployeeNotFoundException();
        }
        var resultDto = _mapper.Map<Employee, EmployeeDto>(existedEmployee);
        return resultDto;
    }

    public async Task<EmployeeDto> Update(string employeeCode, EmployeeDto employeeDto)
    {
        await _unitOfWork.BeginTransactionAsync();
        var existedEmployee = _employeeRepositories.GetEmployeeByCode(employeeCode);
        if (existedEmployee == null)
        {
            throw new EmployeeNotFoundException();
        }

        var anotherEmployee = _employeeRepositories.GetEmployeeByPhone(employeeDto.MobilePhone);
        if (anotherEmployee != null && existedEmployee.Code != anotherEmployee.Code && anotherEmployee.Status == "Active")
        {
            throw new EmployeeDuplicatedPhoneException();
        }

        if (!string.IsNullOrEmpty(employeeDto.Email))
        {
            anotherEmployee = _employeeRepositories.GetEmployeeByEmail(employeeDto.Email);
            if (anotherEmployee != null && existedEmployee.Code != anotherEmployee.Code && anotherEmployee.Status == "Active")
            {
                throw new EmployeeDuplicatedEmailException();
            }
        }

        var newEmployee = _mapper.Map<EmployeeDto, Employee>(employeeDto);
        newEmployee.Code = employeeCode;
        var result = _employeeRepositories.UpdateEmployee(newEmployee);
        var resultDto = _mapper.Map<Employee, EmployeeDto>(result);

        await _unitOfWork.CommitTransactionAsync();
        return resultDto;
    }

    public Employee Delete(string code)
    {
        var existedEmployee = _employeeRepositories.GetEmployeeByCode(code);
        if (existedEmployee == null)
        {
            throw new EmployeeNotFoundException();
        }
        return _employeeRepositories.DeleteEmployee(code);
    }
    public async Task<int> AddAvatarPicture(IFormFileCollection items,string code)
    {
        await _unitOfWork.BeginTransactionAsync();
        var existedEmployee = _employeeRepositories.GetEmployeeByCode(code);
        if (items.Count > 0)
        {
            foreach (var formFile in items)
            {
                if (formFile.Length > 0)
                {
                    var folder = @"\" + "Resources" + @"\" + "Avatar" + @"\";
                    var createTime = DateTime.Now.ToString("MMddHHss");
                    var fileName = folder + createTime + "_" + formFile.FileName;
                    var fullPath = Environment.CurrentDirectory + fileName;
                    existedEmployee.AvatarPicture = createTime + "_" + formFile.FileName;
                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            var result = _employeeRepositories.UpdateEmployee(existedEmployee);
            await _unitOfWork.CommitTransactionAsync();
            return 1;
        }
        else
        { 
            return 0; 
        }
    }
    public async Task<int> AddIdentityPicture(IFormFileCollection items, string code)
    {
        await _unitOfWork.BeginTransactionAsync();
        var existedEmployee = _employeeRepositories.GetEmployeeByCode(code);
        if (items.Count > 0)
        {
            foreach (var formFile in items)
            {
                if (formFile.Length > 0)
                {
                    var folder = @"\" + "Resources" + @"\" + "Identity" + @"\";
                    var createTime = DateTime.Now.ToString("MMddHHss");
                    var fileName = folder + createTime + "_" + formFile.FileName;
                    var fullPath = Environment.CurrentDirectory + fileName;
                    existedEmployee.IdentityNumberPicture = createTime + "_" + formFile.FileName;
                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            var result = _employeeRepositories.UpdateEmployee(existedEmployee);
            await _unitOfWork.CommitTransactionAsync();
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public async Task<int> AddLicensePicture(IFormFileCollection items, string code)
    {
        await _unitOfWork.BeginTransactionAsync();
        var existedEmployee = _employeeRepositories.GetEmployeeByCode(code);
        if (items.Count > 0)
        {
            foreach (var formFile in items)
            {
                if (formFile.Length > 0)
                {
                    var folder = @"\" + "Resources" + @"\" + "License" + @"\";
                    var createTime = DateTime.Now.ToString("MMddHHss");
                    var fileName = folder + createTime + "_" + formFile.FileName;
                    var fullPath = Environment.CurrentDirectory + fileName;
                    existedEmployee.DrivingLicensePicture = createTime + "_" + formFile.FileName;
                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            var result = _employeeRepositories.UpdateEmployee(existedEmployee);
            await _unitOfWork.CommitTransactionAsync();
            return 1;
        }
        else
        {
            return 0;
        }
    }
}