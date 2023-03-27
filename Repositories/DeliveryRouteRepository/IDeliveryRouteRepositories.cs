using Repositories;
using Databases.Entities;
using Services.Models.DeliveryRoute;

namespace Repositories.DeliveryRouteRepository;

public interface IDeliveryRouteRepositories : IRepository<DeliveryRoute, int>
{
    IQueryable<DeliveryRoute> GetAllDeliveryRoutes(DeliveryRouteQuery query);
    DeliveryRoute? GetDeliveryRouteById(int id);
    void Create(DeliveryRoute DeliveryRoute);
    DeliveryRoute DeleteDeliveryRoute(int id);
    DeliveryRoute UpdateDeliveryRoute(DeliveryRoute DeliveryRoute);
}