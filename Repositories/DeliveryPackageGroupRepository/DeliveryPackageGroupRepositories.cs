using Databases;
using Databases.Entities;
using Services.Models.DeliveryPackageGroup;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliveryPackageGroupRepository;

public class DeliveryPackageGroupRepositories : Repository<DeliveryPackageGroup, string>, IDeliveryPackageGroupRepositories
{
    public DeliveryPackageGroupRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliveryPackageGroup deliverPackageGroup)
    {
        deliverPackageGroup.Code = "DPG" + Guid.NewGuid().ToString("n").Substring(0, 8);
        Add(deliverPackageGroup);
        UnitOfWork.SaveChanges();
    }

    public IQueryable<DeliveryPackageGroup> GetAllDeliveryPackageGroups(DeliveryPackageGroupQuery queryData)
    {
        IQueryable<DeliveryPackageGroup> query = GetAll();

        if (queryData.Code != null)
        {
            var pattern = $"%{queryData.Code}%";
            query = query.Where(q => EF.Functions.Like(q.Code, pattern));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        return query;
    }

    public DeliveryPackageGroup DeleteDeliveryPackageGroup(string code)
    {
        var deliverPackageGroup = GetAll().First(e => e.Code == code);
        Delete(deliverPackageGroup);
        UnitOfWork.SaveChanges();
        return deliverPackageGroup;
    }

    public DeliveryPackageGroup UpdateDeliveryPackageGroup(DeliveryPackageGroup deliverPackageGroup)
    {
        Update(deliverPackageGroup);
        UnitOfWork.SaveChanges();
        return deliverPackageGroup;
    }

    public DeliveryPackageGroup? GetDeliveryPackageGroupByCode(string code)
    {
        return GetAll().FirstOrDefault(e => e.Code == code);
    }
}