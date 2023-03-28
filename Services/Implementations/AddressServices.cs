using AutoMapper;
using Databases.Entities;
using Repositories.AddressRepository;
using Services.Interfaces;
using Services.Models.Address;
using Services.Models.Pagination;

namespace Services.Implementations;

public class AddressServices : IAddressServices
{
    private readonly ICommonServices _commonServices;
    private readonly IAddressRepositories _addressRepositories;
    private readonly IMapper _mapper;

    public AddressServices(
        ICommonServices commonServices,
        IAddressRepositories addressRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _addressRepositories = addressRepositories;
        _mapper = mapper;
    }


    public Address Create(AddressDto addressDto)
    {
        var address = _mapper.Map<AddressDto, Address>(addressDto);
        _addressRepositories.Create(address);
        return address;
    }

    public async Task<PaginatedResultDto<Address>> GetAll(PaginationQuery query)
    {
        var queryable = _addressRepositories.GetAll();
        var result = _commonServices.CreatePaginationResponse<Address>(queryable, query);
        return result;
    }

    public Address Update(Guid id, AddressDto addressDto)
    {
        var newAddress = _mapper.Map<AddressDto, Address>(addressDto);
        newAddress.Id = id;
        var result = _addressRepositories.UpdateAddress(newAddress);
        return result;
    }

    public Address Delete(Guid id)
    {
        return _addressRepositories.DeleteAddress(id);
    }
}
