using Repositories;
using Databases.Entities;
using Services.Models.DeliveryPackage;

namespace Repositories.DeliveryPackageRepository;

public interface IDeliveryPackageRepositories : IRepository<DeliveryPackage, string>
{
    IQueryable<DeliveryPackage> GetAllDeliveryPackages(DeliveryPackageQuery query);
    DeliveryPackage? GetDeliveryPackageByCode(string code);
    void Create(DeliveryPackage DeliveryPackage);
    DeliveryPackage DeleteDeliveryPackage(string id);
    DeliveryPackage UpdateDeliveryPackage(DeliveryPackage DeliveryPackage);
}