using AutoMapper;
using Databases.Entities;
using Services.Models.Delivering.Session;
using Services.Models.DeliverySessionLine;

namespace Mapper.Profiles;

public class DeliverySessionLineProfile : Profile
{
    public DeliverySessionLineProfile()
    {
        CreateMap<DeliverySessionLine, DeliverySessionLineDto>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<DeliverySessionLine, DeliveringSessionLineDto>().ReverseMap().IgnoreAllNonExisting();
    }
}