using AutoMapper;
using Databases.Entities;
using Services.Models.Address;

namespace Mapper.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressDto, Address>().ReverseMap().IgnoreAllNonExisting();
    }
}
