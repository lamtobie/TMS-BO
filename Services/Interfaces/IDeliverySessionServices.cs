using Databases.Entities;
using Services.Models.DeliverySession;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IDeliverySessionServices
{
    Task<PaginatedResultDto<DeliverySessionDto>> GetAll(DeliverySessionQuery query);
    Task<PaginatedResultDto<DeliverySessionTreeResultDto>> GetTree(DeliverySessionQuery query);
    Task<DeliverySessionDto> GetOneByCode(string code);
    Task<DeliverySessionDto> Create(DeliverySessionToCreateDto deliverySessionDto);
    Task<DeliverySessionDto> UpdateSessionLines(string deliverySessionCode, DeliverySessionDto deliverySessionDto);
    Task<DeliverySessionDto> HandedOver(string deliverySessionCode, DeliverySessionConfirmDto data);
    Task<DeliverySessionDto> Cancel(string deliverySessionCode, DeliverySessionConfirmDto data);
    Task<DeliverySessionDto> AssignDriverToDOs(AssignDriverDto data);
    Task<List<DeliverySession>> CreateDropOffSession(DeliverySession sessionPickup);
}