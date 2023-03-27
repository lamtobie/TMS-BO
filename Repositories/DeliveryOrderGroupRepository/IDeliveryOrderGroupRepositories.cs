using Repositories;
using Databases.Entities;
using Services.Models.DeliveryOrderGroup;

namespace Repositories.DeliveryOrderGroupRepository;

public interface IDeliveryOrderGroupRepositories : IRepository<DeliveryOrderGroup, string>
{
    IQueryable<DeliveryOrderGroup> GetAllDeliveryOrderGroups(DeliveryOrderGroupQuery queryData);
    DeliveryOrderGroup? GetOneByCode(string code);
}