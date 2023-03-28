using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.DeliveryOrder;
using Services.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryOrderController : BaseController
    {
        private readonly IDeliveryOrderServices _deliveryOrderServices;

        public DeliveryOrderController(
          //  IAuthorizedServices authorizedServices,
            IDeliveryOrderServices deliveryOrderServices
            ) : base()
        {
            _deliveryOrderServices = deliveryOrderServices;
        }

        [HttpGet("[action]")]
        public async Task<PaginatedResultDto<DeliveryOrderTreeResultDto>> GetAll([FromQuery] DeliveryOrderQuery query)
        {
            var result = await _deliveryOrderServices.GetTree(query);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<PaginatedResultDto<SearchPersonResponseDto>> SearchPerson([FromQuery] SearchPersonQuery query)
        {
            var result = await _deliveryOrderServices.GetDeliveryOrderReponsibility(query);
            return result;
        }

        [HttpGet("[action]/{deliveryOrderCode}")]
        public async Task<BaseModel<DeliveryOrderDto>> GetOne([FromRoute] string deliveryOrderCode)
        {
            var result = await _deliveryOrderServices.GetOneByCode(deliveryOrderCode);
            var response = new BaseModel<DeliveryOrderDto>()
            {
                Data = result
            };
            return response;
        }

        // [HttpPost("[action]")]
        // public async Task<BaseModel<DeliveryOrderDto>> Create([FromBody] DeliveryOrderDto deliveryOrder)
        // {
        //     var result = await _deliveryOrderServices.Create(deliveryOrder);
        //     var response = new BaseModel<DeliveryOrderDto>()
        //     {
        //         Data = result
        //     };
        //     return response;
        // }

        [HttpPost("[action]")]
        public async Task<BaseModel<DeliveryOrderDto>> CreateManyDropoff([FromBody] DeliveryOrderManyDropoffCreationDto data)
        {
            var result = await _deliveryOrderServices.CreateManyDropoff(data);
            var response = new BaseModel<DeliveryOrderDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpPost("[action]")]
        public async Task<BaseModel<DeliveryOrderDto>> CreateManyInTransit([FromBody] DeliveryOrderInTransitCreationDto data)
        {
            var result = await _deliveryOrderServices.CreateManyInTransit(data);
            var response = new BaseModel<DeliveryOrderDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpDelete("[action]/{deliveryOrderCode}")]
        public async Task<BaseModel<DeliveryOrder>> Delete([FromRoute] string deliveryOrderCode)
        {
            var result = _deliveryOrderServices.Delete(deliveryOrderCode);
            var response = new BaseModel<DeliveryOrder>()
            {
                Data = result
            };
            return response;
        }

        [HttpPatch("[action]/{deliveryOrderCode}")]
        public async Task<BaseModel<DeliveryOrderDto>> Update([FromBody] DeliveryOrderDto deliveryOrder, [FromRoute] string deliveryOrderCode)
        {
            var result = await _deliveryOrderServices.Update(deliveryOrderCode, deliveryOrder);
            var response = new BaseModel<DeliveryOrderDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpPost("[action]/{deliveryOrderCode}")]
        public async Task<BaseModel<DeliveryOrderDto>> ChangeStatus([FromBody] DeliveryOrderUpdateStatusDto data, [FromRoute] string deliveryOrderCode)
        {
            var result = await _deliveryOrderServices.ChangeStatus(deliveryOrderCode, data);
            var response = new BaseModel<DeliveryOrderDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpPost("[action]")]
        public async Task<BaseModel<List<DeliveryOrderDto>>> ScanCode([FromBody] List<string> codes)
        {
            var result = await _deliveryOrderServices.ScanCode(codes);
            var response = new BaseModel<List<DeliveryOrderDto>>()
            {
                Data = result
            };
            return response;
        }
    }
}