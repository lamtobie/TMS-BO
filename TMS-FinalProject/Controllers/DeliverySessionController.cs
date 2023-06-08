using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.DeliverySession;
using Services.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverySessionController: ControllerBase
    {
        private readonly IDeliverySessionServices _deliverySessionServices;

        public DeliverySessionController(
           // IAuthorizedServices authorizedServices,
            IDeliverySessionServices deliverySessionServices
            ) : base()
        {
            _deliverySessionServices = deliverySessionServices;
        }

        [HttpGet("[action]")]
        public async Task<PaginatedResultDto<DeliverySessionDto>> GetAll([FromQuery] DeliverySessionQuery query)
        {
            var result = await _deliverySessionServices.GetAll(query);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<PaginatedResultDto<DeliverySessionTreeResultDto>> GetTree([FromQuery] DeliverySessionQuery query)
        {
            var result = await _deliverySessionServices.GetTree(query);
            return result;
        }

        [HttpGet("[action]/{deliverySessionCode}")]
        public async Task<BaseModel<DeliverySessionDto>> GetOne([FromRoute] string deliverySessionCode)
        {
            var result = await _deliverySessionServices.GetOneByCode(deliverySessionCode);
            var response = new BaseModel<DeliverySessionDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpPost("[action]")]
        public async Task<BaseModel<DeliverySessionDto>> Create([FromBody] DeliverySessionToCreateDto deliverySession)
        {
            var result = await _deliverySessionServices.Create(deliverySession);
            var response = new BaseModel<DeliverySessionDto>()
            {
                Data = result
            };
            return response;
        }

        // [HttpDelete("[action]/{deliverySessionCode}")]
        // public async Task<BaseModel<DeliverySession>> Delete([FromRoute] string deliverySessionCode)
        // {
        //     var result = _deliverySessionServices.Delete(deliverySessionCode);
        //     var response = new BaseModel<DeliverySession>()
        //     {
        //         Data = result
        //     };
        //     return response;
        // }

        //[HttpPatch("[action]/{deliverySessionCode}")]
        //public async Task<BaseModel<DeliverySessionDto>> Update([FromBody] DeliverySessionDto deliverySession, [FromRoute] string deliverySessionCode)
        //{
        //    var result = await _deliverySessionServices.Update(deliverySessionCode, deliverySession);
        //    var response = new BaseModel<DeliverySessionDto>()
        //    {
        //        Data = result
        //    };
        //    return response;
        //}

        [HttpPatch("[action]/{deliverySessionCode}")]
        public async Task<BaseModel<DeliverySessionDto>> UpdateSessionLines([FromRoute] string deliverySessionCode, [FromBody] DeliverySessionDto deliverySession)
        {
            var result = await _deliverySessionServices.UpdateSessionLines(deliverySessionCode, deliverySession);
            var response = new BaseModel<DeliverySessionDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpPost("[action]/{deliverySessionCode}")]
        public async Task<BaseModel<DeliverySessionDto>> HandedOver([FromRoute] string deliverySessionCode, [FromBody] DeliverySessionConfirmDto data)
        {
            var result = await _deliverySessionServices.HandedOver(deliverySessionCode, data);
            var response = new BaseModel<DeliverySessionDto>()
            {
                Data = result
            };
            return response;
        }

        // [HttpPost("[action]/{deliverySessionCode}")]
        // public async Task<BaseModel<DeliverySessionDto>> AssignDriverToSession([FromBody] AssignDriverDto data, [FromRoute] string deliverySessionCode)
        // {
        //     var result = await _deliverySessionServices.AssignDriverToSession(deliverySessionCode, data);
        //     var response = new BaseModel<DeliverySessionDto>()
        //     {
        //         Data = result
        //     };
        //     return response;
        // }

        [HttpPost("[action]")]
        public async Task<BaseModel<DeliverySessionDto>> AssignDriverToDO([FromBody] AssignDriverDto data)
        {
            var result = await _deliverySessionServices.AssignDriverToDOs(data);
            var response = new BaseModel<DeliverySessionDto>()
            {
                Data = result
            };
            return response;
        }

        // [HttpPost("[action]/{deliverySessionCode}")]
        // public async Task<BaseModel<DeliverySessionDto>> InTransit([FromRoute] string deliverySessionCode)
        // {
        //     var result = await _deliverySessionServices.InTransit(deliverySessionCode);
        //     var response = new BaseModel<DeliverySessionDto>()
        //     {
        //         Data = result
        //     };
        //     return response;
        // }

        [HttpPost("[action]/{deliverySessionCode}")]
        public async Task<BaseModel<DeliverySessionDto>> Cancel([FromRoute] string deliverySessionCode, [FromBody] DeliverySessionConfirmDto data)
        {
            var result = await _deliverySessionServices.Cancel(deliverySessionCode, data);
            var response = new BaseModel<DeliverySessionDto>()
            {
                Data = result
            };
            return response;
        }

        [HttpPost("[action]/{deliverySessionCode}")]
        public async Task<BaseModel<DeliverySessionDto>> Returned([FromRoute] string deliverySessionCode, [FromBody] DeliverySessionConfirmDto data)
        {
            var result = await _deliverySessionServices.Returned(deliverySessionCode, data);
            var response = new BaseModel<DeliverySessionDto>()
            {
                Data = result
            };
            return response;
        }
        // [HttpGet("[action]/{deliverySessionCode}")]
        // public async Task<BaseModel<DeliverySessionDto>> GetHistory([FromRoute] string deliverySessionCode)
        // {
        //     var result = await _deliverySessionServices.GetOneByCode(deliverySessionCode);
        //     var response = new BaseModel<DeliverySessionDto>()
        //     {
        //         Data = result
        //     };
        //     return response;
        // }
    }
}