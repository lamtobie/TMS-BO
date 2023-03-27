using Databases;
using Databases.Entities;
using Services.Models.DeliveryRoute;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliveryRouteRepository;

public class DeliveryRouteRepositories : Repository<DeliveryRoute, int>, IDeliveryRouteRepositories
{
    public DeliveryRouteRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliveryRoute deliverRoute)
    {
        // deliverRoute.Id = "DP" + Guid.NewGuid().ToString("n").Substring(0, 8);
        Add(deliverRoute);
        UnitOfWork.SaveChanges();
    }

    public IQueryable<DeliveryRoute> GetAllDeliveryRoutes(DeliveryRouteQuery queryData)
    {
        IQueryable<DeliveryRoute> query = GetAll();

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        return query;
    }

    public DeliveryRoute DeleteDeliveryRoute(int id)
    {
        var deliverRoute = GetAll().First(e => e.Id == id);
        Delete(deliverRoute);
        UnitOfWork.SaveChanges();
        return deliverRoute;
    }

    public DeliveryRoute UpdateDeliveryRoute(DeliveryRoute deliverRoute)
    {
        Update(deliverRoute);
        UnitOfWork.SaveChanges();
        return deliverRoute;
    }

    public DeliveryRoute? GetDeliveryRouteById(int id)
    {
        return GetAll().FirstOrDefault(e => e.Id == id);
    }
}