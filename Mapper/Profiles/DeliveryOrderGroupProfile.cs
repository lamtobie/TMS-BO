using AutoMapper;
using Databases.Entities;
using Services.Models.DeliveryOrderGroup;

namespace Mapper.Profiles;

public class DeliveryOrderGroupProfile : Profile
{
    public DeliveryOrderGroupProfile()
    {
        CreateMap<DeliveryOrderGroupDto, DeliveryOrderGroup>()
            .ReverseMap()
            .IgnoreAllNonExisting();
        CreateMap<DeliveryOrderGroupCreationDto, DeliveryOrderGroup>()
            .IgnoreAllNonExisting();
    }
}