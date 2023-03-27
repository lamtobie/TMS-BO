using Repositories;
using Databases.Entities;
using Services.Models.DeliveryPackageGroup;

namespace Repositories.DeliveryPackageGroupRepository;

public interface IDeliveryPackageGroupRepositories : IRepository<DeliveryPackageGroup, string>
{
    IQueryable<DeliveryPackageGroup> GetAllDeliveryPackageGroups(DeliveryPackageGroupQuery query);
    DeliveryPackageGroup? GetDeliveryPackageGroupByCode(string code);
    void Create(DeliveryPackageGroup DeliveryPackageGroup);
    DeliveryPackageGroup DeleteDeliveryPackageGroup(string id);
    DeliveryPackageGroup UpdateDeliveryPackageGroup(DeliveryPackageGroup DeliveryPackageGroup);
}