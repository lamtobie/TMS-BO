using Services.Models.DeliveryOrderGroup;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IDeliveryOrderGroupServices
{
    Task<PaginatedResultDto<DeliveryOrderGroupDto>> GetAll(DeliveryOrderGroupQuery queryData);
    Task<DeliveryOrderGroupDto> Create(DeliveryOrderGroupCreationDto deliveryOrderGroupCreationDto);
    Task<DeliveryOrderGroupDto> GetOne(string code);
    Task<DeliveryOrderGroupDto> Cancel(string code, DeliveryOrderGroupCancelDto dto);
    Task<DeliveryOrderGroupDto> Update(DeliveryOrderGroupUpdateDto deliveryOrderGroupUpdateDto, string code);
}