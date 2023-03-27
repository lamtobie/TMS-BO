using Repositories;
using Databases.Entities;
using Services.Models.Delivering.Session;
using Services.Models.DeliverySession;

namespace Repositories.DeliverySessionRepository;

public interface IDeliverySessionRepositories : IRepository<DeliverySession, string>
{
    IQueryable<DeliverySession> GetAllDeliverySessionsForDelivering(DeliveringSessionQuery queryData);
    IQueryable<DeliverySession> GetAllDeliverySessions(DeliverySessionQuery query);
    DeliverySession? GetDeliverySessionByCode(string code);
    DeliverySession? GetSessionForHandedOver(string code);
    List<DeliverySession> GetChildrenDSsByCode(string code);
    void Create(DeliverySession DeliverySession);
    DeliverySession DeleteDeliverySession(string code);
    DeliverySession UpdateDeliverySession(DeliverySession deliverySession);
}