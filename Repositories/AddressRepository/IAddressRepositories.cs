using Databases.Entities;
using Repositories;
using Services.Models.Station;

namespace Repositories.AddressRepository;

public interface IAddressRepositories : IRepository<Address, Guid>
{
    IQueryable<Address> GetAllAddress(AddressQuery queryData);
    void Create(Address Address);
    Address DeleteAddress(Guid id);
    Address UpdateAddress(Address Address);
}
