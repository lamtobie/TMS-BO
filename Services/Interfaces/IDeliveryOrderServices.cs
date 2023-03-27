using Databases.Entities;
using Services.Models.DeliveryOrder;
using Services.Models.DeliverySession;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IDeliveryOrderServices
{
    Task<PaginatedResultDto<DeliveryOrder>> GetAll(DeliveryOrderQuery query);
    Task<PaginatedResultDto<SearchPersonResponseDto>> GetDeliveryOrderReponsibility(SearchPersonQuery query);
    Task<PaginatedResultDto<DeliveryOrderTreeResultDto>> GetTree(DeliveryOrderQuery query);
    Task<DeliveryOrderDto> GetOneByCode(string code);
    Task<DeliveryOrderDto> CreateManyDropoff(DeliveryOrderManyDropoffCreationDto data);
    Task<DeliveryOrderDto> CreateManyInTransit(DeliveryOrderInTransitCreationDto data);
    DeliveryOrder Delete(string code);
    Task<DeliveryOrderDto> Update(string code, DeliveryOrderDto deliveryOrderDto);
    void UpdateDOBySession(DeliverySessionDto sessionDto);
    Task<DeliveryOrderDto> ChangeStatus(string deliveryOrderCode, DeliveryOrderUpdateStatusDto data);
    Task<List<DeliveryOrderDto>> ScanCode(List<string> codes);
    Task<List<DeliveryOrder>> RemoveDOFromGroup(List<DeliveryOrder> deliveryOrders);
    Task<List<DeliveryOrder>> UpdateDOGroupCode(List<DeliveryOrder> deliveryOrders, DeliveryOrderGroup deliveryOrderGroup);
}