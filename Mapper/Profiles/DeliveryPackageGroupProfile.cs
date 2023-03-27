using AutoMapper;
using Databases.Entities;
using Services.Models.DeliveryPackageGroup;

namespace Mapper.Profiles;

public class DeliveryPackageGroupProfile : Profile
{
    public DeliveryPackageGroupProfile()
    {
        CreateMap<DeliveryPackageGroup, DeliveryPackageGroupDto>().ReverseMap().IgnoreAllNonExisting();
        // CreateMap<DeliveryPackageGroup, DeliveryPackageGroupCreationDto>().ReverseMap().IgnoreAllNonExisting();
    }
}