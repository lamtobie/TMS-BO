using Databases;
using Databases.Entities;
using Services.Models.DeliverySessionLine;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliverySessionLineRepository;

public class DeliverySessionLineRepositories : Repository<DeliverySessionLine, Guid>, IDeliverySessionLineRepositories
{
    public DeliverySessionLineRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DeliverySessionLine deliverSessionLine)
    {
        deliverSessionLine.Code = "DSL" + Guid.NewGuid().ToString("n").Substring(0, 8);
        Add(deliverSessionLine);
        UnitOfWork.SaveChanges();
    }

    public IQueryable<DeliverySessionLine> GetAllDeliverySessionLines(DeliverySessionLineQuery queryData)
    {
        IQueryable<DeliverySessionLine> query = GetAll();

        if (queryData.Keyword != null)
        {
            var pattern = $"%{queryData.Keyword}%";
            query = query.Where(q => EF.Functions.Like(q.Code, pattern));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        return query;
    }

    public DeliverySessionLine DeleteDeliverySessionLine(string code)
    {
        var deliverSessionLine = GetAll().First(e => e.Code == code);
        Delete(deliverSessionLine);
        UnitOfWork.SaveChanges();
        return deliverSessionLine;
    }

    public DeliverySessionLine UpdateDeliverySessionLine(DeliverySessionLine deliverSessionLine)
    {
        Update(deliverSessionLine);
        UnitOfWork.SaveChanges();
        return deliverSessionLine;
    }

    public DeliverySessionLine? GetDeliverySessionLineByCode(string code)
    {
        return GetAll().FirstOrDefault(e => e.Code == code);
    }
}