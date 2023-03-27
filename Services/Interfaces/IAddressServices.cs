using Databases.Entities;
using Services.Models.Address;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IAddressServices
{
    Task<PaginatedResultDto<Address>> GetAll(PaginationQuery query);
    Address Create(AddressDto data);
    Address Delete(Guid id);
    Address Update(Guid id, AddressDto data);
}
