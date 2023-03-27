using AutoMapper;
using Databases.Entities;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.DeliveryOrder;

namespace Mapper.Profiles;

public class DeliveryOrderProfile : Profile
{
    public DeliveryOrderProfile()
    {
        CreateMap<DeliveryOrder, DeliveryOrderDto>()
            .ForMember(dest => dest.SessionStatus, opt => opt.MapFrom(src => src.Session.Status))
            .ReverseMap()
            .IgnoreAllNonExisting();
        CreateMap<DeliveryOrder, DeliveringDeliveryOrderDto>()
            .ForMember(e => e.NumberOfDeliveryPackage, opt => opt.MapFrom(e => e.DeliveryOrderLines.Count))
            .ReverseMap().IgnoreAllNonExisting();
        CreateMap<DeliveryOrder, SearchPersonResponseDto>()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.StartContactPhone ?? src.EndContactPhone))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.StartContactPerson ?? src.EndContactPerson))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.StartAddress ?? src.EndAddress))
            .IgnoreAllNonExisting();
    }
}