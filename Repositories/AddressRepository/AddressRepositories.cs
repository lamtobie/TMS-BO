using Databases;
using Databases.Entities;

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

    public Address UpdateAddress(Address address)
    {
        Update(address);
        UnitOfWork.SaveChanges();
        return address;
    }
}
