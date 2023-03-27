using AutoMapper;
using Databases.Entities;
using Services.Models.Delivering.Vehicle;
using Services.Models.Vehicle;

namespace Mapper.Profiles;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, VehicleDto>()
            .ForPath(e => e.VehicleTypeInformation.Code, atc => atc.MapFrom(src => src.VehicleType.Code))
            .ForPath(e => e.VehicleTypeInformation.Height, atc => atc.MapFrom(src => src.VehicleType.Height))
            .ForPath(e => e.VehicleTypeInformation.Name, atc => atc.MapFrom(src => src.VehicleType.Name))
            .ForPath(e => e.VehicleTypeInformation.Length, atc => atc.MapFrom(src => src.VehicleType.Length))
            .ForPath(e => e.VehicleTypeInformation.Width, atc => atc.MapFrom(src => src.VehicleType.Width))
            .ForPath(e => e.VehicleTypeInformation.MaximumCapacity, atc => atc.MapFrom(src => src.VehicleType.MaximumCapacity))
            .ForPath(e => e.VehicleTypeInformation.MaximumPayload, atc => atc.MapFrom(src => src.VehicleType.MaximumPayload))
            .ForPath(e => e.VehicleTypeInformation.Status, atc => atc.MapFrom(src => src.VehicleType.Status))
            .ReverseMap()
            .IgnoreAllNonExisting();
        CreateMap<Vehicle, VehicleToCreateDto>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<Vehicle, VehicleToUpdateDto>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<Vehicle, DeliveringVehicleDto>().ReverseMap().IgnoreAllNonExisting();
    }
}