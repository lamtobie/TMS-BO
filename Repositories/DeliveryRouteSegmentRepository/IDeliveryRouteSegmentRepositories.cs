using Repositories;
using Databases.Entities;
using Services.Models.DeliveryRouteSegment;

namespace Repositories.DeliveryRouteSegmentRepository;

public interface IDeliveryRouteSegmentRepositories : IRepository<DeliveryRouteSegment, int>
{
    IQueryable<DeliveryRouteSegment> GetAllDeliveryRouteSegments(DeliveryRouteSegmentQuery query);
    DeliveryRouteSegment? GetDeliveryRouteSegmentById(int id);
    void Create(DeliveryRouteSegment DeliveryRouteSegment);
    DeliveryRouteSegment DeleteDeliveryRouteSegment(int id);
    DeliveryRouteSegment UpdateDeliveryRouteSegment(DeliveryRouteSegment DeliveryRouteSegment);
}