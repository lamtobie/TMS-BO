using Databases.Entities;
using Services.Models.DeliveryPackage;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IDeliveryPackageServices
{
    Task<PaginatedResultDto<DeliveryPackage>> GetAll(DeliveryPackageQuery query);
    Task<DeliveryPackage> GetOneByCode(string code);
    DeliveryPackage Create(DeliveryPackageDto deliveryPackageDto);
    DeliveryPackage Delete(string code);
    DeliveryPackage Update(string code, DeliveryPackageDto deliveryPackageDto);
}