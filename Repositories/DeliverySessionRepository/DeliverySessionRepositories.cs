using Databases;
using Databases.Entities;
using Services.Helper.Enums;
using Services.Models.Delivering.Session;
using Services.Models.Delivery.Session;
using Services.Models.DeliverySession;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliverySessionRepository;

public class DeliverySessionRepositories : Repository<DeliverySession, string>, IDeliverySessionRepositories
{
    public DeliverySessionRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliverySession deliverSession)
    {
        // deliverSession.Code = "DS" + Guid.NewGuid().ToString("n").Substring(0, 8);
        Add(deliverSession);
        // UnitOfWork.SaveChanges();
    }

    // public string RandomSessionCode()
    // {
    //     return "DS" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    // }

    // public DeliverySession CreateSession(DeliverySession? parent = null, string? groupCode = null)
    // {
    //     var session = new DeliverySession();

    //     session.Code = RandomSessionCode();

    //     if (parent != null)
    //     {
    //         session.ParentCode = parent.Code;
    //     }

    //     if (groupCode != null)
    //     {
    //         session.SessionGroupCode = groupCode;
    //     }

    //     session.Status = SessionStatusEnum.New.ToString();

    //     return session;
    // }

    public IQueryable<DeliverySession> GetAllDeliverySessionsForDelivering(DeliveringSessionQuery queryData)
    {
        IQueryable<DeliverySession> query = GetAll()
            .Include(x => x.DeliverySessionLines)
            .Include(x => x.Childrens)
            .ThenInclude(x => x.DeliverySessionLines)
            .Include(x => x.Vehicle)
            .ThenInclude(x => x.VehicleType)
            .Include(x => x.Driver)
            .Include(x => x.Coordinator)
            .Include(x => x.StartStation)
            .Include(x => x.EndStation)
            .Include(e => e.DeliveryOrders)
            .Where(e => 
                (e.SessionType == SessionTypeEnum.Pickup.ToString() && e.Status == SessionStatusEnum.HandedOver.ToString())
            );

        if (queryData.Keyword != null)
        {
            var keywords = queryData.Keyword.Split(",");
            query = query.Where(q => keywords.Contains(q.Code) ||
                                     (q.ParentCode != null && keywords.Contains(q.ParentCode)) ||
                                     q.Childrens.Any(x => keywords.Contains(x.Code)));
        }

        if (queryData.IsTransit)
        {
            query = query.Where(e => e.ToCustomer == false);
        }
        
        if (queryData.SessionStatus.Count > 0)
        {
            query = query.Where(e => queryData.SessionStatus.Any(k => k == e.Status));
        }
        
        if (queryData.CreatedAt != null && queryData.GetTimeRange<DeliverySessionQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<DeliverySessionQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1] ||
                                     x.Childrens.Any(y => y.CreatedAt >= range[0] && y.CreatedAt <= range[1]));
        }
        
        return query;
    }

    
    public IQueryable<DeliverySession> GetAllDeliverySessions(DeliverySessionQuery queryData)
    {
        IQueryable<DeliverySession> query = GetAll()
                .Include(x => x.DeliverySessionLines)
                .Include(x => x.Childrens)
                .ThenInclude(x => x.DeliverySessionLines)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.VehicleType)
                .Include(x => x.Driver)
                .Include(x => x.Coordinator)
                .Include(x => x.StartStation)
                .Include(x => x.EndStation);

        if (queryData.Keyword != null)
        {
            query = query.Where(q => q.Code == queryData.Keyword ||
                        q.ParentCode == queryData.Keyword ||
                        q.DeliverySessionLines.Any(l => l.DeliveryPackageCode == queryData.Keyword) ||
                        q.DeliverySessionLines.Any(l => l.DeliveryOrderCode == queryData.Keyword) ||
                        q.Childrens.Any(c => c.Code == queryData.Keyword) ||
                        q.Childrens.Any(c => c.ParentCode == queryData.Keyword) ||
                        q.Childrens.Any(c => c.DeliverySessionLines.Any(l => l.DeliveryPackageCode == queryData.Keyword)) ||
                        q.Childrens.Any(c => c.DeliverySessionLines.Any(l => l.DeliveryOrderCode == queryData.Keyword)));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status ||
                        q.Childrens.Any(c => c.Status == queryData.Status));
        }

        if (queryData.CoordinatorCode != null)
        {
            query = query.Where(q => q.CoordinatorCode == queryData.CoordinatorCode ||
                        q.Childrens.Any(c => c.CoordinatorCode == queryData.CoordinatorCode));
        }

        if (queryData.DriverCode != null)
        {
            query = query.Where(q => q.DriverCode == queryData.DriverCode ||
                        q.Childrens.Any(c => c.DriverCode == queryData.DriverCode));
        }

        if (queryData.StartStationCode != null)
        {
            query = query.Where(q => q.StartStationCode == queryData.StartStationCode ||
                        q.Childrens.Any(c => c.StartStationCode == queryData.StartStationCode));
        }

        if (queryData.EndStationCode != null)
        {
            query = query.Where(q => q.EndStationCode == queryData.EndStationCode ||
                        q.Childrens.Any(c => c.EndStationCode == queryData.EndStationCode));
        }

        if (queryData.CreatedAt != null && queryData.GetTimeRange<DeliverySessionQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<DeliverySessionQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1] ||
                                x.Childrens.Any(y => y.CreatedAt >= range[0] && y.CreatedAt <= range[1]));
        }

        return query;
    }

    public DeliverySession DeleteDeliverySession(string code)
    {
        var deliverSession = GetAll().First(e => e.Code == code);
        Delete(deliverSession);
        UnitOfWork.SaveChanges();
        return deliverSession;
    }

    public DeliverySession UpdateDeliverySession(DeliverySession deliverSession)
    {
        Update(deliverSession);
        UnitOfWork.SaveChanges();
        return deliverSession;
    }

    public DeliverySession? GetDeliverySessionByCode(string code)
    {
        return GetAll()
                .Include(x => x.DeliverySessionLines)
                .Include(x => x.Childrens)
                .ThenInclude(x => x.DeliverySessionLines)
                .Include(x => x.Vehicle)
                .Include(x => x.Driver)
                .Include(x => x.Coordinator)
                .Include(x => x.StartStation)
                .Include(x => x.EndStation)
                .AsNoTracking()
                .FirstOrDefault(e => e.Code == code);
    }

    public DeliverySession? GetSessionForHandedOver(string code)
    {
        return GetAll()
            .Include(e => e.DeliveryOrders)
            .FirstOrDefault(e => e.Code == code);
    }

    public List<DeliverySession> GetChildrenDSsByCode(string code)
    {
        return GetAll().Where(x => x.ParentCode == code).ToList();
    }
}