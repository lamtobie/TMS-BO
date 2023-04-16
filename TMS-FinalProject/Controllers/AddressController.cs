using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Pagination;
using Services.Models.Station;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : BaseController
{
    private readonly IAddressServices _addressServices;

    public AddressController(
     //   IAuthorizedServices authorizedServices,
        IAddressServices addressServices
        ) : base()
    {
        _addressServices = addressServices;
    }

    [HttpGet("[action]")]
    public async Task<PaginatedResultDto<Address>> GetAll([FromQuery] AddressQuery query)
    {
        var result = await _addressServices.GetAllAddress(query);
        return result;
    }
}
