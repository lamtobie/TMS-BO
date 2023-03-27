using AutoMapper;
using Databases.Entities;
using Services.Models.DeliveryPackage;

namespace Mapper.Profiles;

public class DeliveryPackageProfile : Profile
{
    public DeliveryPackageProfile()
    {
        CreateMap<DeliveryPackage, DeliveryPackageDto>().ReverseMap().IgnoreAllNonExisting();
        // CreateMap<DeliveryPackage, DeliveryPackageCreationDto>().ReverseMap().IgnoreAllNonExisting();
    }
}