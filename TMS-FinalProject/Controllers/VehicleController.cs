using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Pagination;
using Services.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : BaseController
{
    private readonly IVehicleServices _vehicleServices;
    public VehicleController(
       // IAuthorizedServices authorizedServices, 
        IVehicleServices vehicleServices
        ) : base()
    {
        _vehicleServices = vehicleServices;
    }

    [HttpGet("[action]")]
    public async Task<PaginatedResultDto<VehicleDto>> GetAll([FromQuery] VehicleQuery query)
    {
        var result = await _vehicleServices.GetAll(query);
        return result;
    }

    [HttpGet("[action]/{code}")]
    public async Task<BaseModel<VehicleDto>> GetOne([FromRoute] string code)
    {
        var result = await _vehicleServices.GetOneByCode(code);
        var response = new BaseModel<VehicleDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpPost("[action]")]
    public async Task<BaseModel<VehicleDto>> Create([FromBody] VehicleToCreateDto vehicleDto)
    {
        var result = await _vehicleServices.Create(vehicleDto);
        var response = new BaseModel<VehicleDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpPatch("[action]/{code}")]
    public async Task<BaseModel<VehicleDto>> Update([FromBody] VehicleToUpdateDto vehicleDto, [FromRoute] string code)
    {
        var result = await _vehicleServices.Update(vehicleDto, code);
        var response = new BaseModel<VehicleDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpDelete("[action]/{code}")]
    public async Task<BaseModel<VehicleDto>> Delete([FromRoute] string code)
    {
        var result = await _vehicleServices.Delete(code);
        var response = new BaseModel<VehicleDto>()
        {
            Data = result
        };
        return response;
    }
}