using Databases.Entities;
using Services.Models.Address;
using Services.Models.Pagination;
using Services.Models.Station;

namespace Services.Interfaces;

public interface IAddressServices
{
    Task<PaginatedResultDto<Address>> GetAllAddress(AddressQuery query);
    Address Create(AddressDto data);
    Address Delete(Guid id);
    Address Update(Guid id, AddressDto data);
}
