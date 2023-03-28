using AutoMapper;
using Repositories;
using Databases.Entities;
using Repositories.DeliveryRouteRepository;
using Services.Interfaces;
using Services.Models.DeliveryRoute;
using Services.Models.Pagination;

namespace Services.Implementations;

public class DeliveryRouteServices : IDeliveryRouteServices
{
    private readonly ICommonServices _commonServices;

    private readonly IDeliveryRouteRepositories _deliveryRouteRepositories;

    // Mapper
    private readonly IMapper _mapper;

    public DeliveryRouteServices(
        ICommonServices commonServices,
        IDeliveryRouteRepositories deliveryRouteRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _deliveryRouteRepositories = deliveryRouteRepositories;
        _mapper = mapper;
    }

    public DeliveryRoute Create(DeliveryRouteDto deliveryRouteDto)
    {
        var deliveryRoute = _mapper.Map<DeliveryRouteDto, DeliveryRoute>(deliveryRouteDto);
        _deliveryRouteRepositories.Create(deliveryRoute);
        return deliveryRoute;
    }

    public async Task<PaginatedResultDto<DeliveryRoute>> GetAll(DeliveryRouteQuery query)
    {
        var queryable = _deliveryRouteRepositories.GetAllDeliveryRoutes(query);
        var result = _commonServices.CreatePaginationResponse<DeliveryRoute>(queryable, query);
        return result;
    }

    public async Task<DeliveryRoute> GetOneById(int id)
    {
        var result = _deliveryRouteRepositories.GetDeliveryRouteById(id);
        return result;
    }

    public DeliveryRoute Update(int id, DeliveryRouteDto deliveryRouteDto)
    {
        var newDeliveryRoute = _mapper.Map<DeliveryRouteDto, DeliveryRoute>(deliveryRouteDto);
        var entityDeliveryRoute = _deliveryRouteRepositories.GetDeliveryRouteById(id);
        entityDeliveryRoute = newDeliveryRoute;
        _deliveryRouteRepositories.UpdateDeliveryRoute(entityDeliveryRoute);
        return entityDeliveryRoute;
    }

    public DeliveryRoute Delete(int id)
    {
        return _deliveryRouteRepositories.DeleteDeliveryRoute(id);
    }
}