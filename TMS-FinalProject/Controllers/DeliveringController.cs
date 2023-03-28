using Services.Helper.Enums;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.Delivering.DeliveryOrder.ActionDelivering;
using Services.Models.Delivering.Session;
using Services.Models.Delivering.Session.SessionAction;
using Services.Models.Delivery.Session;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveringController : BaseController
{
    private readonly IDeliveringServices _deliveringServices;

    public DeliveringController(
        //IAuthorizedServices authorizedServices,
        IDeliveringServices deliveringServices) : base()
    {
        _deliveringServices = deliveringServices;
    }

    [HttpGet("[action]")]
    public async Task<BaseModel<DeliveringDOViewModel>> GetAllDO([FromQuery] DeliveringDOQuery query)
    {
        var result = await _deliveringServices.GetAllDO(query);
        var response = new BaseModel<DeliveringDOViewModel>()
        {
            Data = result
        };
        return response;
    }

    [HttpGet("[action]/{code}")]
    public async Task<BaseModel<DeliveringDeliveryOrderDto>> GetOneDO(string code)
    {
        var result = await _deliveringServices.GetOneDO(code);
        var response = new BaseModel<DeliveringDeliveryOrderDto>()
        {
            Data = result
        };
        return response;
    }

    [HttpPatch("[action]/{code}")]
    public async Task<BaseModel<DeliveringDeliveryOrderDto>> UpdateDeliveryOrderStatus([FromRoute] string code, [FromBody] dynamic dto)
    {
        var result = await _deliveringServices.UpdateDeliveryOrderStatus(code, dto);
        var response = new BaseModel<DeliveringDeliveryOrderDto>()
        {
            Data = result
        };
        return response;
    }
    
    [HttpGet("[action]")]
    public async Task<BaseModel<DeliveringSessionViewModel>> GetAllSession([FromQuery] DeliveringSessionQuery query)
    {
        var result = await _deliveringServices.GetAllSession(query);
        var response = new BaseModel<DeliveringSessionViewModel>()
        {
            Data = result
        };
        return response;
    }
    
    [HttpGet("[action]/{code}")]
    public async Task<BaseModel<DeliveringDeliverySessionDto>> GetOneSession(string code)
    {
        var result = await _deliveringServices.GetOneSession(code);
        var response = new BaseModel<DeliveringDeliverySessionDto>()
        {
            Data = result
        };
        return response;
    }
    
    [HttpPatch("[action]/{code}")]
    public async Task<BaseModel<DeliveringDeliverySessionDto>> UpdateSessionStatus([FromRoute] string code, [FromBody] SessionActionDto dto)
    {
        var result = await _deliveringServices.UpdateSessionStatus(code, dto);
        var response = new BaseModel<DeliveringDeliverySessionDto>()
        {
            Data = result
        };
        return response;
    } 
}