using AutoMapper;
using Databases.Entities;
using Services.Models.Delivering.VehicleType;
using Services.Models.VehicleType;

namespace Mapper.Profiles;

public class VehicleTypeProfile : Profile
{
    public VehicleTypeProfile()
    {
        CreateMap<VehicleTypeDto, VehicleType>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<VehicleType, DeliveringVehicleTypeDto>().ReverseMap().IgnoreAllNonExisting();
    }
}