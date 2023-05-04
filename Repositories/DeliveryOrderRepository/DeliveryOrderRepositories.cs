using Databases;
using Databases.Entities;
using Services.Helper.Enums;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.DeliveryOrder;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliveryOrderRepository;

public class DeliveryOrderRepositories : Repository<DeliveryOrder, string>, IDeliveryOrderRepositories
{
    public DeliveryOrderRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliveryOrder deliverOrder)
    {
        deliverOrder.Code = RandomDOCode();
        Add(deliverOrder);
        UnitOfWork.SaveChanges();
    }
    private string RandomDOCode()
    {
        return "DO" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }

    public IQueryable<DeliveryOrder> GetAllDeliveryOrdersForDelivering(DeliveringDOQuery queryData)
    {
        IQueryable<DeliveryOrder> query = GetAll()
            .Include(e => e.Childrens)
            .Where(e => 
                e.DriverCode != null
            );
        
        if (queryData.Keyword != null)
        {
            var keywords = queryData.Keyword.Split(",");
            query = query.Where(q => keywords.Contains(q.Code) ||
                 (q.ParentCode != null && keywords.Contains(q.ParentCode)) ||
                 q.Childrens.Any(x => keywords.Contains(x.Code)));
        }

        if (queryData.DeliveryAddress != null)
        {
            query = query.Where(e => e.EndAddress!.Text.ToLower().Contains(queryData.DeliveryAddress.ToLower()));
        }

        if (queryData.Province != null)
        {
            query = query.Where(e => e.EndAddress!.Text.ToLower().Contains(queryData.Province.ToLower()));
        }

        if (queryData.District != null)
        {
            query = query.Where(e => e.EndAddress!.Text.ToLower().Contains(queryData.District.ToLower()));
        }

        if (queryData.DeliveryServices.Count > 0)
        {
            query = query.Where(e => queryData.DeliveryServices.Any(k => k == e.ThreePLTeam));
        }

        if (queryData.DeliveryStatus.Count > 0)
        {
            query = query.Where(e => queryData.DeliveryStatus.Any(k => k == e.Status));
        }

        if (queryData.CreatedAt != null && queryData.GetTimeRange<DeliveringDOQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<DeliveringDOQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1] ||
                                     x.Childrens.Any(y => y.CreatedAt >= range[0] && y.CreatedAt <= range[1]));
        }

        return query;
    }

    public IQueryable<DeliveryOrder> GetAllDeliveryOrders(DeliveryOrderQuery queryData)
    {
        IQueryable<DeliveryOrder> query = GetAll();
;

        if (queryData.Keyword != null)
        {
            var keywords = queryData.Keyword.Split(",");
            query = query.Where(q => keywords.Contains(q.Code) ||
                (q.ParentCode != null && keywords.Contains(q.ParentCode)) ||
                q.Childrens.Any(x => keywords.Contains(x.Code)));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status || q.Childrens.Any(x => x.Status == queryData.Status));
        }

        if (queryData.DriverCode != null)
        {
            query = query.Where(q => q.DriverCode == queryData.DriverCode || q.Childrens.Any(x => x.DriverCode == queryData.DriverCode));
        }

        if (queryData.CoordinatorCode != null)
        {
            query = query.Where(q => q.CoordinatorCode == queryData.CoordinatorCode || q.Childrens.Any(x => x.CoordinatorCode == queryData.CoordinatorCode));
        }

        if (queryData.ThreePLTeam != null)
        {
            query = query.Where(q => q.ThreePLTeam == queryData.ThreePLTeam || q.Childrens.Any(x => x.ThreePLTeam == queryData.ThreePLTeam));
        }

        if (queryData.SourceBy != null)
        {
            query = query.Where(q => q.SourceBy == queryData.SourceBy || q.Childrens.Any(x => x.SourceBy == queryData.SourceBy));
        }

        if (queryData.NumberOfRoute != null)
        {
            if (queryData.NumberOfRoute.Contains(","))
            {
                var routes = queryData.NumberOfRoute.Split(",");
                query = query.Where(q => routes.Contains(q.Childrens.Count.ToString()));
            }
            else
            {
                query = query.Where(q => q.Childrens.Count == Int32.Parse(queryData.NumberOfRoute));
            }
        }

        if (queryData.StartStation != null)
        {
            query = query.Where(q => q.StartAddress.Text.Contains(queryData.StartStation) ||
                                    q.StartStationCode.Contains(queryData.StartStation) ||
                                    q.Childrens.Any(x => x.StartAddress.Text.Contains(queryData.StartStation) ||
                                                        x.StartContactPerson.Contains(queryData.StartStation)));
        }

        if (queryData.ActualArrivalTime != null && queryData.GetTimeRange<DeliveryOrderQuery>("ActualArrivalTime").Count > 0)
        {
            var range = queryData.GetTimeRange<DeliveryOrderQuery>("ActualArrivalTime");
            query = query.Where(x => (x.ActualArrivalTime >= range[0] && x.ActualArrivalTime <= range[1]) ||
                                x.Childrens.Any(y => y.ActualArrivalTime >= range[0] && y.ActualArrivalTime <= range[1]));
        }

        if (queryData.CreatedAt != null && queryData.GetTimeRange<DeliveryOrderQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<DeliveryOrderQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1] ||
                                x.Childrens.Any(y => y.CreatedAt >= range[0] && y.CreatedAt <= range[1]));
        }

        if (queryData.ActualStartTime != null && queryData.GetTimeRange<DeliveryOrderQuery>("ActualStartTime").Count > 0)
        {
            var range = queryData.GetTimeRange<DeliveryOrderQuery>("ActualStartTime");
            query = query.Where(x => x.ActualStartTime >= range[0] && x.ActualStartTime <= range[1] ||
                                x.Childrens.Any(y => y.ActualStartTime >= range[0] && y.ActualStartTime <= range[1]));
        }

        return query;
    }

    public IQueryable<DeliveryOrder> GetAllDeliveryOrderReponsibility(SearchPersonQuery queryData)
    {
        IQueryable<DeliveryOrder> query = GetAll();

        if (!string.IsNullOrEmpty(queryData.Phone))
        {
            query = query.Where(x => x.StartContactPhone == queryData.Phone || x.EndContactPhone == queryData.Phone);
        }

        if (!string.IsNullOrEmpty(queryData.FullName))
        {
            query = query.Where(x => x.StartContactPerson == queryData.FullName || x.EndContactPerson == queryData.FullName);
        }

        return query;
    }

    public DeliveryOrder DeleteDeliveryOrder(string code)
    {
        var deliverOrder = GetAll().First(e => e.Code == code);
        Delete(deliverOrder);
        UnitOfWork.SaveChanges();
        return deliverOrder;
    }

    public DeliveryOrder UpdateDeliveryOrder(DeliveryOrder deliverOrder)
    {
        Update(deliverOrder);
        UnitOfWork.SaveChanges();
        return deliverOrder;
    }

    public DeliveryOrder? GetDeliveryOrderByCode(string code)
    {
        return GetAll()
                .Include(x => x.Session).ThenInclude(x => x.Driver)
                .Include(x => x.Session).ThenInclude(x => x.Coordinator)
                .Include(x => x.DeliveryOrderLines).ThenInclude(x => x.DeliveryPackage)
                .Include(x => x.Childrens).ThenInclude(x => x.DeliveryOrderLines).ThenInclude(x => x.DeliveryPackage)
                .Include(x => x.StartAddress)
                .Include(x => x.EndAddress)
                .Include(x => x.Childrens).ThenInclude(x => x.StartAddress)
                .Include(x => x.Childrens).ThenInclude(x => x.EndAddress)
                .FirstOrDefault(e => e.Code == code);
    }

    // public List<DeliveryOrder> GetChildrenDOsByCode(string code)
    // {
    //     return GetAll()
    //             .Where(x => x.ParentCode == code)
    //             .ToList();
    // }

    // public DeliveryOrder UnassignSession(string code)
    // {
    //     var deliverOrder = GetAll().Where(x => x.Code == code).AsNoTracking().FirstOrDefault();
    //     if (deliverOrder != null)
    //     {
    //         deliverOrder.SessionCode = null;
    //         Update(deliverOrder);
    //     }
    //     return deliverOrder;
    // }

    // public List<DeliveryOrder> UnassignSession(List<string> codes)
    // {
    //     var deliverOrders = GetAll().Where(x => codes.Contains(x.Code)).AsNoTracking().ToList();
    //     if (deliverOrders != null)
    //     {
    //         deliverOrders.ForEach(x =>
    //         {
    //             x.SessionCode = null;
    //             x.Status = DeliveryOrderStatusEnum.New.ToString();
    //         });
    //         UpdateRange(deliverOrders);
    //     }
    //     return deliverOrders;
    // }

    // public DeliveryOrder AssignSession(string code, string sessionCode)
    // {
    //     var deliverOrder = GetAll().Where(x => x.Code == code).AsNoTracking().FirstOrDefault();
    //     if (deliverOrder != null)
    //     {
    //         deliverOrder.SessionCode = sessionCode;
    //         Update(deliverOrder);
    //     }
    //     return deliverOrder;
    // }

    // public List<DeliveryOrder> AssignSession(List<string> codes, string sessionCode)
    // {
    //     var deliverOrders = GetAll().Where(x => codes.Contains(x.Code)).AsNoTracking().ToList();
    //     if (deliverOrders != null && deliverOrders.Count > 0)
    //     {
    //         deliverOrders.ForEach(x =>
    //         {
    //             x.SessionCode = sessionCode;
    //             // x.Status = DeliveryOrderStatusEnum.Picking.ToString();
    //         });
    //         UpdateRange(deliverOrders);
    //     }
    //     return deliverOrders;
    // }

    // public void UpdateStatusBySession(DeliverySession session)
    // {
    //     var nextStatus = DeliveryOrderStatusEnum.New.ToString();

    //     if (session.SessionType == SessionTypeEnum.Pickup.ToString() && session.Status == SessionStatusEnum.AConfirmed.ToString())
    //     {
    //         nextStatus = DeliveryOrderStatusEnum.Picking.ToString();
    //     }
    //     else if (session.SessionType == SessionTypeEnum.Pickup.ToString() && session.Status == SessionStatusEnum.BConfirmed.ToString())
    //     {
    //         nextStatus = DeliveryOrderStatusEnum.Picked.ToString();
    //     }
    //     else if (session.SessionType == SessionTypeEnum.Pickup.ToString() && session.Status == SessionStatusEnum.Cancelled.ToString())
    //     {
    //         nextStatus = DeliveryOrderStatusEnum.New.ToString();
    //     }
    //     else if (session.SessionType == SessionTypeEnum.Dropoff.ToString() && session.Status == SessionStatusEnum.AConfirmed.ToString())
    //     {
    //         if (session.EndStationCode != null)
    //         {
    //             nextStatus = DeliveryOrderStatusEnum.MovingToStation.ToString();
    //         }
    //         else
    //         {
    //             nextStatus = DeliveryOrderStatusEnum.DeliveringToClient.ToString();
    //         }
    //     }
    //     else if (session.SessionType == SessionTypeEnum.Dropoff.ToString() && session.Status == SessionStatusEnum.BConfirmed.ToString())
    //     {
    //         if (session.EndStationCode != null)
    //         {
    //             nextStatus = DeliveryOrderStatusEnum.DeliveredToStationSuccess.ToString();
    //         }
    //         else
    //         {
    //             nextStatus = DeliveryOrderStatusEnum.DeliveredToClientSuccess.ToString();
    //         }
    //     }
    //     else if (session.SessionType == SessionTypeEnum.Dropoff.ToString() && session.Status == SessionStatusEnum.Cancelled.ToString())
    //     {
    //         if (session.EndStationCode != null)
    //         {
    //             nextStatus = DeliveryOrderStatusEnum.DeliveredToStationFailure.ToString();
    //         }
    //         else
    //         {
    //             nextStatus = DeliveryOrderStatusEnum.DeliveredToClientFailure.ToString();
    //         }
    //     }

    //     session.DeliveryOrders.ToList().ForEach(x =>
    //     {
    //         x.Status = nextStatus;
    //     });
    // }
}