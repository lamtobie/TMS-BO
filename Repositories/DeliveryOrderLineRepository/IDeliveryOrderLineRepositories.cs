using Repositories;
using Databases.Entities;

namespace Repositories.DeliveryOrderLineRepository;

public interface IDeliveryOrderLineRepositories : IRepository<DeliveryOrderLine, Guid>
{
    void Create(DeliveryOrderLine deliveryOrderLine);
    DeliveryOrderLine DeleteDeliveryOrderLine(Guid id);
    DeliveryOrderLine UpdateDeliveryOrderLine(DeliveryOrderLine deliveryOrderLine);
}