using AutoMapper;
using Repositories;
using Databases.Entities;
using Repositories.DeliveryPackageRepository;
using Services.Interfaces;
using Services.Models.DeliveryPackage;
using Services.Models.Pagination;

namespace Services.Implementations;

public class DeliveryPackageServices : IDeliveryPackageServices
{
    private readonly ICommonServices _commonServices;

    private readonly IDeliveryPackageRepositories _deliveryPackageRepositories;

    // Mapper
    private readonly IMapper _mapper;

    public DeliveryPackageServices(
        ICommonServices commonServices,
        IDeliveryPackageRepositories deliveryPackageRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _deliveryPackageRepositories = deliveryPackageRepositories;
        _mapper = mapper;
    }

    public DeliveryPackage Create(DeliveryPackageDto deliveryPackageDto)
    {
        var deliveryPackage = _mapper.Map<DeliveryPackageDto, DeliveryPackage>(deliveryPackageDto);
        _deliveryPackageRepositories.Create(deliveryPackage);
        return deliveryPackage;
    }

    public async Task<PaginatedResultDto<DeliveryPackage>> GetAll(DeliveryPackageQuery query)
    {
        var queryable = _deliveryPackageRepositories.GetAllDeliveryPackages(query);
        var result = _commonServices.CreatePaginationResponse<DeliveryPackage>(queryable, query);
        return result;
    }

    public async Task<DeliveryPackage> GetOneByCode(string code)
    {
        var result = _deliveryPackageRepositories.GetDeliveryPackageByCode(code);
        return result;
    }

    public DeliveryPackage Update(string deliveryPackageCode, DeliveryPackageDto deliveryPackageDto)
    {
        var newDeliveryPackage = _mapper.Map<DeliveryPackageDto, DeliveryPackage>(deliveryPackageDto);
        var entityDeliveryPackage = _deliveryPackageRepositories.GetDeliveryPackageByCode(deliveryPackageCode);
        entityDeliveryPackage = newDeliveryPackage;
        _deliveryPackageRepositories.UpdateDeliveryPackage(entityDeliveryPackage);
        return entityDeliveryPackage;
    }

    public DeliveryPackage Delete(string code)
    {
        return _deliveryPackageRepositories.DeleteDeliveryPackage(code);
    }
}