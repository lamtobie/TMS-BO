using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Pagination;
using Services.Models.VehicleType;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleTypeController : BaseController
{
    private readonly IVehicalTypeServices _vehicalTypeServices;

    public VehicleTypeController(
        IVehicalTypeServices vehicalTypeServices
        ) : base()
    {
        _vehicalTypeServices = vehicalTypeServices;
    }

    [HttpGet("[action]")]
    public async Task<PaginatedResultDto<VehicleTypeDto>> GetAll([FromQuery] VehicleTypeQuery query)
    {
        var result = await _vehicalTypeServices.GetAllVehicleType(query);
        return result;
    }

    [HttpPost("[action]")]
    public async Task<BaseModel<VehicleType>> Create([FromBody] VehicleTypeDto vehicleType)
    {
        var result = _vehicalTypeServices.CreateVehicleType(vehicleType);
        var response = new BaseModel<VehicleType>()
        {
            Data = result
        };
        return response;
    }

    [HttpGet("[action]/{vehicleTypeCode}")]
    public async Task<BaseModel<VehicleTypeDto>> GetOne([FromRoute] string vehicleTypeCode)
    {
        var result = await _vehicalTypeServices.GetVehicleTypeByCode(vehicleTypeCode);
        var response = new BaseModel<VehicleTypeDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpDelete("[action]/{vehicalTypeCode}")]
    public async Task<BaseModel<VehicleTypeDto>> Delete([FromRoute] string vehicalTypeCode)
    {
        var result = _vehicalTypeServices.DeleteVehicleType(vehicalTypeCode);
        var response = new BaseModel<VehicleTypeDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpPatch("[action]/{vehicleTypeCode}")]
    public async Task<BaseModel<VehicleTypeDto>> Update([FromBody] VehicleTypeDto vehicleType, [FromRoute] string vehicleTypeCode)
    {
        var result = _vehicalTypeServices.UpdateVehicleType(vehicleTypeCode, vehicleType);
        var response = new BaseModel<VehicleTypeDto>()
        {
            Data = result
        };
        return response;
    }
}
