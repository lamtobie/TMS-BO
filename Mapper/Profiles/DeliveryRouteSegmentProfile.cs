using AutoMapper;
using Databases.Entities;
using Services.Models.DeliveryRouteSegment;

namespace Mapper.Profiles;

public class DeliveryRouteSegmentProfile : Profile
{
    public DeliveryRouteSegmentProfile()
    {
        CreateMap<DeliveryRouteSegment, DeliveryRouteSegmentDto>().ReverseMap().IgnoreAllNonExisting();
        // CreateMap<DeliveryRouteSegment, DeliveryRouteSegmentCreationDto>().ReverseMap().IgnoreAllNonExisting();
    }
}