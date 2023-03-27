using Databases.Entities;
using Repositories;

namespace Repositories.AddressRepository;

public interface IAddressRepositories : IRepository<Address, Guid>
{
    void Create(Address Address);
    Address DeleteAddress(Guid id);
    Address UpdateAddress(Address Address);
}
