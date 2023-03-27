using Databases.Entities;
using Services.Models.DeliveryRoute;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IDeliveryRouteServices
{
    Task<PaginatedResultDto<DeliveryRoute>> GetAll(DeliveryRouteQuery query);
    Task<DeliveryRoute> GetOneById(int id);
    DeliveryRoute Create(DeliveryRouteDto deliveryRouteDto);
    DeliveryRoute Delete(int id);
    DeliveryRoute Update(int id, DeliveryRouteDto deliveryRouteDto);
}