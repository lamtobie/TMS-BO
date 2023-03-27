using Databases;
using Databases.Entities;

namespace Repositories.DeliveryOrderLineRepository;

public class DeliveryOrderLineRepositories : Repository<DeliveryOrderLine, Guid>, IDeliveryOrderLineRepositories
{
    public DeliveryOrderLineRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliveryOrderLine deliverOrder)
    {
        deliverOrder.Id = Guid.NewGuid();
        Add(deliverOrder);
        UnitOfWork.SaveChanges();
    }

    public DeliveryOrderLine DeleteDeliveryOrderLine(Guid id)
    {
        var deliverOrder = GetAll().First(e => e.Id == id);
        Delete(deliverOrder);
        UnitOfWork.SaveChanges();
        return deliverOrder;
    }

    public DeliveryOrderLine UpdateDeliveryOrderLine(DeliveryOrderLine deliverOrder)
    {
        Update(deliverOrder);
        UnitOfWork.SaveChanges();
        return deliverOrder;
    }
}