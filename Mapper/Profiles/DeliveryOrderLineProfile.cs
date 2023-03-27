using AutoMapper;
using Databases.Entities;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.DeliveryOrderLine;

namespace Mapper.Profiles;

public class DeliveryOrderLineProfile : Profile
{
    public DeliveryOrderLineProfile()
    {
        CreateMap<DeliveryOrderLine, DeliveryOrderLineDto>()
            .ForMember(des => des.Code, act => act.MapFrom(src => src.DeliveryPackage.Code))
            .ForMember(des => des.ExternalCode, act => act.MapFrom(src => src.DeliveryPackage.ExternalCode))
            .ForMember(des => des.Name, act => act.MapFrom(src => src.DeliveryPackage.Name))
            .ForMember(des => des.Uom, act => act.MapFrom(src => src.DeliveryPackage.Uom))
            .ForMember(des => des.PackageStatus, act => act.MapFrom(src => src.DeliveryPackage.Status))
            .ReverseMap()
            .IgnoreAllNonExisting();
        CreateMap<DeliveryOrderLine, DeliveringDOLineDto>().ReverseMap().IgnoreAllNonExisting();

    }
}