using Services.Models.Delivering.DeliveryOrder;
using Services.Models.Delivering.Session;
using Services.Models.Delivering.Session.SessionAction;
using Services.Models.Delivery.Session;

namespace Services.Interfaces;

public interface IDeliveringServices
{
    Task<DeliveringDOViewModel> GetAllDO(DeliveringDOQuery query);
    Task<DeliveringDeliveryOrderDto> GetOneDO(string code);
    Task<DeliveringDeliveryOrderDto> UpdateDeliveryOrderStatus(string code, dynamic dto);
    Task<DeliveringSessionViewModel> GetAllSession(DeliveringSessionQuery query);
    Task<DeliveringDeliverySessionDto> GetOneSession(string code);
    Task<DeliveringDeliverySessionDto> UpdateSessionStatus(string code, SessionActionDto dto);
}