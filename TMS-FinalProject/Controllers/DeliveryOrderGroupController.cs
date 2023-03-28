using Services.Interfaces;
using Services.Models.Base;
using Services.Models.DeliveryOrderGroup;
using Services.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveryOrderGroupController : BaseController
{
    private readonly IDeliveryOrderGroupServices _deliveryOrderGroupServices;
    
    public DeliveryOrderGroupController(//IAuthorizedServices authorizedServices,
                                        IDeliveryOrderGroupServices deliveryOrderGroupServices) : base()
    {
        _deliveryOrderGroupServices = deliveryOrderGroupServices;
    }

    [HttpGet("[action]")]
    public async Task<PaginatedResultDto<DeliveryOrderGroupDto>> GetAll([FromQuery] DeliveryOrderGroupQuery query)
    {
        var result = await _deliveryOrderGroupServices.GetAll(query);
        return result;
    }

    [HttpGet("[action]/{code}")]
    public async Task<BaseModel<DeliveryOrderGroupDto>> GetOne([FromRoute] string code)
    {
        var result = await _deliveryOrderGroupServices.GetOne(code);
        var response = new BaseModel<DeliveryOrderGroupDto>()
        {
            Data = result
        };
        return response;
    }
    
    [HttpPost("[action]")]
    public async Task<BaseModel<DeliveryOrderGroupDto>> Create(DeliveryOrderGroupCreationDto deliveryOrderGroupCreationDto)
    {
        var result = await _deliveryOrderGroupServices.Create(deliveryOrderGroupCreationDto);
        var response = new BaseModel<DeliveryOrderGroupDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpPatch("[action]/{code}")]
    public async Task<BaseModel<DeliveryOrderGroupDto>> Cancel([FromRoute] string code, [FromBody] DeliveryOrderGroupCancelDto dto)
    {
        var result = await _deliveryOrderGroupServices.Cancel(code, dto);
        var response = new BaseModel<DeliveryOrderGroupDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpPatch("[action]/{code}")]
    public async Task<BaseModel<DeliveryOrderGroupDto>> Update(
        [FromBody] DeliveryOrderGroupUpdateDto dto,
        [FromRoute] string code)
    {
        var result = await _deliveryOrderGroupServices.Update(dto, code);
        var response = new BaseModel<DeliveryOrderGroupDto>()
        {
            Data = result
        };
        return response;
    }
    
}