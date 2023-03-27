using AutoMapper;
using Databases.Entities;
using Services.Models.Delivery.Session;
using Services.Models.DeliverySession;

namespace Mapper.Profiles;

public class DeliverySessionProfile : Profile
{
    public DeliverySessionProfile()
    {
        CreateMap<DeliverySession, DeliverySessionDto>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<DeliverySession, DeliveringDeliverySessionDto>()
            .ReverseMap().IgnoreAllNonExisting();
    }
}