using Repositories;
using Databases.Entities;
using Services.Models.Station;

namespace Repositories.StationRepository;

public interface IStationRepositories : IRepository<Station, string>
{
    IQueryable<Station> GetAllStation(StationQuery queryData);
    void CreateStation(Station station);
    Station? GetStationByCode(string code);
    Station DeleteStation(string code);
    Station UpdateStation(Station station);
}
