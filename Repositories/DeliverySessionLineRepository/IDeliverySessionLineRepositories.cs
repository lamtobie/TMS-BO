using Repositories;
using Databases.Entities;
using Services.Models.DeliverySessionLine;

namespace Repositories.DeliverySessionLineRepository;

public interface IDeliverySessionLineRepositories : IRepository<DeliverySessionLine, Guid>
{
    DeliverySessionLine? GetDeliverySessionLineByCode(string code);
    void Create(DeliverySessionLine deliverySessionLine);
    DeliverySessionLine DeleteDeliverySessionLine(string code);
    DeliverySessionLine UpdateDeliverySessionLine(DeliverySessionLine deliverySessionLine);
}