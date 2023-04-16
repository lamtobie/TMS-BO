using Databases;
using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Models.Station;

namespace Repositories.AddressRepository;

public class AddressRepositories : Repository<Address, Guid>, IAddressRepositories
{
    public AddressRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }


    public void Create(Address address)
    {
        Add(address);
        UnitOfWork.SaveChanges();
    }

    public Address DeleteAddress(Guid id)
    {
        var address = GetAll().First(e => e.Id == id);
        Delete(address);
        UnitOfWork.SaveChanges();
        return address;
    }

    public IQueryable<Address> GetAllAddress(AddressQuery queryData)
    {
            IQueryable<Address> query = GetAll();
            return query;
    }

    public Address UpdateAddress(Address address)
    {
        Update(address);
        UnitOfWork.SaveChanges();
        return address;
    }
}
