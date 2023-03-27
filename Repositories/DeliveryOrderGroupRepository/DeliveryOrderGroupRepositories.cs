using Databases;
using Databases.Entities;
using Services.Models.DeliveryOrderGroup;
using Microsoft.EntityFrameworkCore;
using Repositories.DeliveryOrderGroupRepository;

namespace Repositories.DeliveryOrderGroupRepository;

public class DeliveryOrderGroupRepositories : Repository<DeliveryOrderGroup, string>, IDeliveryOrderGroupRepositories
{
    public DeliveryOrderGroupRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public IQueryable<DeliveryOrderGroup> GetAllDeliveryOrderGroups(DeliveryOrderGroupQuery queryData)
    {
        var query = GetAll();
        if (queryData.Keyword != null)
        {
            query = query.Where(e =>
                e.Code.Contains(queryData.Keyword));
        }

        if (queryData.Status != null)
        {
            query = query.Where(e => e.Status == queryData.Status);
        }

        //query = queryData.QueryByCreatedAt<DeliveryOrderGroup>(query);

        Func<DeliveryOrderGroup, DeliveryOrderGroup> getNotNull = (e) =>
        {
            e.DeliveryOrders = e.DeliveryOrders.Where(d => d.ParentCode == null).ToList();
            return e;
        };
        
        query = query
            .Include(e => e.DeliveryOrders)
            .ThenInclude(e => e.Childrens)
            .ThenInclude(e => e.DeliveryOrderLines)
            .ThenInclude(e => e.DeliveryPackage);
        
        return query.AsEnumerable().Select(getNotNull).AsQueryable();
    }

    public DeliveryOrderGroup? GetOneByCode(string code)
    {
        var deliveryOrderGroup = GetAll()
            .Include(e => e.DeliveryOrders.Where(e => e.ParentCode == null))
            .ThenInclude(e => e.Childrens)
            .ThenInclude(e => e.DeliveryOrderLines)
            .ThenInclude(e => e.DeliveryPackage)
            .AsNoTracking()
            .FirstOrDefault(e => e.Code == code);
        return deliveryOrderGroup;
    }
}