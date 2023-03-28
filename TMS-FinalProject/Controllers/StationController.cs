using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Pagination;
using Services.Models.Station;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StationController : BaseController
{
    private readonly IStationServices _stationServices;

    public StationController(
     //   IAuthorizedServices authorizedServices,
        IStationServices stationServices
        ) : base()
    {
        _stationServices = stationServices;
    }

    [HttpGet("[action]")]
    public async Task<PaginatedResultDto<Station>> GetAll([FromQuery] StationQuery query)
    {
        var result = await _stationServices.GetAllStation(query);
        return result;
    }

    [HttpPost("[action]")]
    public async Task<BaseModel<StationDto>> Create([FromBody] StationDto station)
    {
        var result = await _stationServices.CreateStation(station);
        var response = new BaseModel<StationDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpGet("[action]/{stationCode}")]
    public async Task<BaseModel<StationDto>> GetOne([FromRoute] string stationCode)
    {
        var result = await _stationServices.GetStationsByCode(stationCode);
        var response = new BaseModel<StationDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpDelete("[action]/{stationCode}")]
    public async Task<BaseModel<Station>> Delete([FromRoute] string stationCode)
    {
        var result = _stationServices.DeleteStation(stationCode);
        var response = new BaseModel<Station>()
        {
            Data = result
        };
        return response;
    }

    [HttpPatch("[action]/{stationCode}")]
    public async Task<BaseModel<StationDto>> Update([FromBody] StationDto stationDto, [FromRoute] string stationCode)
    {
        var result = await _stationServices.UpdateStation(stationDto, stationCode);
        var response = new BaseModel<StationDto>()
        {
            Data = result
        };
        return response;
    }
}
