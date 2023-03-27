using AutoMapper;
using Databases.Entities;
using Services.Models.DeliverySessionGroup;

namespace Mapper.Profiles;

public class DeliverySessionGroupProfile : Profile
{
    public DeliverySessionGroupProfile()
    {
        CreateMap<DeliverySessionGroup, DeliverySessionGroupDto>().ReverseMap().IgnoreAllNonExisting();
    }
}