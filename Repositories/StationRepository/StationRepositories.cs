using Databases;
using Databases.Entities;
using Services.Models.Station;
using Microsoft.EntityFrameworkCore;

namespace Repositories.StationRepository;

public class StationRepositories : Repository<Station, string>, IStationRepositories
{
    public StationRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void CreateStation(Station station)
    {
        Add(station);
        UnitOfWork.SaveChanges();
    }

    public Station DeleteStation(string code)
    {
        var data = GetAll().First(e => e.Code == code);
        Delete(data);
        UnitOfWork.SaveChanges();
        return data;
    }

    public IQueryable<Station> GetAllStation(StationQuery queryData)
    {
        IQueryable<Station> query = GetAll();

        if (queryData.Keyword != null)
        {
            var pattern = $"%{queryData.Keyword}%";
            query = query.Where(q => EF.Functions.Like(q.Code, pattern) ||
                                EF.Functions.Like(q.Name, pattern) ||
                                EF.Functions.Like(q.ContactPhone, pattern));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        return query;
    }

    public Station? GetStationByCode(string code)
    {
        return GetAll().Include(e => e.Address).FirstOrDefault(e => e.Code == code);
    }

    public Station UpdateStation(Station station)
    {
        Update(station);
        return station;
    }
}
