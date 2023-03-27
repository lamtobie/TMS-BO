using Databases;
using Databases.Entities;

namespace Repositories.DeliverySessionGroupRepository;

public interface IDeliverySessionGroupRepositories : IRepository<DeliverySessionGroup, string>
{
    void Create(DeliverySessionGroup DeliverySessionGroup);
    DeliverySessionGroup Init();
}