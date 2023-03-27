using Databases;
using Databases.Entities;
using Services.Models.DeliveryRouteSegment;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliveryRouteSegmentRepository;

public class DeliveryRouteSegmentRepositories : Repository<DeliveryRouteSegment, int>, IDeliveryRouteSegmentRepositories
{
    public DeliveryRouteSegmentRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliveryRouteSegment deliverRouteSegment)
    {
        // deliverRouteSegment.Id = "DP" + Guid.NewGuid().ToString("n").Substring(0, 8);
        Add(deliverRouteSegment);
        UnitOfWork.SaveChanges();
    }

    public IQueryable<DeliveryRouteSegment> GetAllDeliveryRouteSegments(DeliveryRouteSegmentQuery queryData)
    {
        IQueryable<DeliveryRouteSegment> query = GetAll();

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        return query;
    }

    public DeliveryRouteSegment DeleteDeliveryRouteSegment(int id)
    {
        var deliverRouteSegment = GetAll().First(e => e.Id == id);
        Delete(deliverRouteSegment);
        UnitOfWork.SaveChanges();
        return deliverRouteSegment;
    }

    public DeliveryRouteSegment UpdateDeliveryRouteSegment(DeliveryRouteSegment deliverRouteSegment)
    {
        Update(deliverRouteSegment);
        UnitOfWork.SaveChanges();
        return deliverRouteSegment;
    }

    public DeliveryRouteSegment? GetDeliveryRouteSegmentById(int id)
    {
        return GetAll().FirstOrDefault(e => e.Id == id);
    }
}