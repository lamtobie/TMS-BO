using Databases;
using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.DataAttributeRepository;

namespace Eton.TMS.OpBackOffice.Repositories.DataAttributeRepository;

public class DataAttributeRepositories : Repository<DataAttribute, Guid>, IDataAttributeRepositories
{
    public DataAttributeRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public void Create(DataAttribute dataAttribute)
    {
        Add(dataAttribute);
        UnitOfWork.SaveChanges();
    }

    public DataAttribute DeleteDataAttribute(Guid id)
    {
        var dataAttribute = GetAll().First(e => e.Id == id);
        Delete(dataAttribute);
        UnitOfWork.SaveChanges();
        return dataAttribute;
    }

    public DataAttribute UpdateDataAttribute(DataAttribute dataAttribute)
    {
        Update(dataAttribute);
        UnitOfWork.SaveChanges();
        return dataAttribute;
    }
}