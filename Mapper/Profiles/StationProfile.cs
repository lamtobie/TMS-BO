using AutoMapper;
using Databases.Entities;
using Services.Models.Delivering.Station;
using Services.Models.Station;

namespace Mapper.Profiles;

public class StationProfile : Profile
{
    public StationProfile()
    {
        CreateMap<Station, StationDto>().ReverseMap().IgnoreAllNonExisting();
        CreateMap<Station, DeliveringStationDto>().ReverseMap().IgnoreAllNonExisting();
    }
}