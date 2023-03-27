using Databases.Entities;
using Services.Models.Pagination;
using Services.Models.Station;

namespace Services.Interfaces;

public interface IStationServices
{
    Task<StationDto> CreateStation(StationDto stationCreationDto);
    Task<PaginatedResultDto<Station>> GetAllStation(StationQuery query);
    Task<StationDto> GetStationsByCode(string code);
    Station DeleteStation(string code);
    Task<StationDto> UpdateStation(StationDto stationDto, string stationCode);
}
