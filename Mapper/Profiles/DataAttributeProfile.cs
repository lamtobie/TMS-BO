using AutoMapper;
using Databases.Entities;
using Mapper;
using Services.Models.DataAttribute;

namespace Mapper.Profiles;

public class DataAttributeProfile : Profile
{
    public DataAttributeProfile()
    {
        CreateMap<DataAttribute, DataAttributeDto>()
            .ForMember(dest => dest.DataValue, opt => opt.MapFrom(src =>
                src.DataType == "boolean"
                    ? (src.DataValue == "True" ? true : false)
                    : (dynamic)src.DataValue
            ))
            .ReverseMap()
            .IgnoreAllNonExisting();
    }
}