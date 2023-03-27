using AutoMapper;
using Databases.Entities;
using Services.Models.DeliveryRoute;

namespace Mapper.Profiles;

public class DeliveryRouteProfile : Profile
{
    public DeliveryRouteProfile()
    {
        CreateMap<DeliveryRoute, DeliveryRouteDto>().ReverseMap().IgnoreAllNonExisting();
        // CreateMap<DeliveryRoute, DeliveryRouteCreationDto>().ReverseMap().IgnoreAllNonExisting();
    }
}