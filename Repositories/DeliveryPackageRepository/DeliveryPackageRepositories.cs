using Databases;
using Databases.Entities;
using Services.Models.DeliveryPackage;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliveryPackageRepository;

public class DeliveryPackageRepositories : Repository<DeliveryPackage, string>, IDeliveryPackageRepositories
{
    public DeliveryPackageRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliveryPackage deliverPackage)
    {
        deliverPackage.Code = "DP" + Guid.NewGuid().ToString("n").Substring(0, 8);
        Add(deliverPackage);
        UnitOfWork.SaveChanges();
    }

    public IQueryable<DeliveryPackage> GetAllDeliveryPackages(DeliveryPackageQuery queryData)
    {
        IQueryable<DeliveryPackage> query = GetAll();

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

    public DeliveryPackage DeleteDeliveryPackage(string code)
    {
        var deliverPackage = GetAll().First(e => e.Code == code);
        Delete(deliverPackage);
        UnitOfWork.SaveChanges();
        return deliverPackage;
    }

    public DeliveryPackage UpdateDeliveryPackage(DeliveryPackage deliverPackage)
    {
        Update(deliverPackage);
        UnitOfWork.SaveChanges();
        return deliverPackage;
    }

    public DeliveryPackage? GetDeliveryPackageByCode(string code)
    {
        return GetAll().FirstOrDefault(e => e.Code == code);
    }
}