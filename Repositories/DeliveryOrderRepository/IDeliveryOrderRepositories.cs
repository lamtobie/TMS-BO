using Repositories;
using Databases.Entities;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.DeliveryOrder;

namespace Repositories.DeliveryOrderRepository;

public interface IDeliveryOrderRepositories : IRepository<DeliveryOrder, string>
{
    IQueryable<DeliveryOrder> GetAllDeliveryOrdersForDelivering(DeliveringDOQuery queryData);
    IQueryable<DeliveryOrder> GetAllDeliveryOrders(DeliveryOrderQuery query);
    IQueryable<DeliveryOrder> GetAllDeliveryOrderReponsibility(SearchPersonQuery query);
    DeliveryOrder? GetDeliveryOrderByCode(string code);
    // List<DeliveryOrder> GetChildrenDOsByCode(string code);
    void Create(DeliveryOrder DeliveryOrder);
    DeliveryOrder DeleteDeliveryOrder(string id);
    DeliveryOrder UpdateDeliveryOrder(DeliveryOrder DeliveryOrder);
    // DeliveryOrder UnassignSession(string code);
    // List<DeliveryOrder> UnassignSession(List<string> codes);
    // DeliveryOrder AssignSession(string code, string sessionCode);
    // List<DeliveryOrder> AssignSession(List<string> codes, string sessionCode);
    // void UpdateStatusBySession(DeliverySession session);
}