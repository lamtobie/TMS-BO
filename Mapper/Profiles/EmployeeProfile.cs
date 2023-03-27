using AutoMapper;
using Databases.Entities;
using Services.Models.Delivering.Employee;
using Services.Models.Employee;

namespace Mapper.Profiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<Employee, EmployeeCreationDto>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<Employee, DeliveringEmployeeDto>().ReverseMap().IgnoreAllNonExisting();
    }
}