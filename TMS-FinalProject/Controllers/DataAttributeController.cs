using Databases.Entities;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.DataAttribute;
using Services.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAttributeController :BaseController
    {
        private readonly IDataAttributeServices _dataAttributeServices;

        public DataAttributeController(
            //IAuthorizedServices authorizedServices,
            IDataAttributeServices dataAttributeServices
            ) : base()
        {
            _dataAttributeServices = dataAttributeServices;
        }

        [HttpGet("[action]")]
        public async Task<PaginatedResultDto<DataAttribute>> GetAll([FromQuery] PaginationQuery query)
        {
            var result = await _dataAttributeServices.GetAll(query);
            return result;
        }

        // [HttpGet("[action]/{DataAttributeCode}")]
        // public async Task<BaseModel<DataAttribute>> GetOne([FromRoute] string DataAttributeCode)
        // {
        //     var result = await _DataAttributeServices.GetOneByCode(DataAttributeCode);
        //     var response = new BaseModel<DataAttribute>()
        //     {
        //         Data = result
        //     };
        //     return response;
        // }

        [HttpPost("[action]")]
        public async Task<BaseModel<DataAttribute>> Create([FromBody] DataAttributeDto dataAttribute)
        {
            var result = _dataAttributeServices.Create(dataAttribute);
            var response = new BaseModel<DataAttribute>()
            {
                Data = result
            };
            return response;
        }

        [HttpDelete("[action]/{DataAttributeCode}")]
        public async Task<BaseModel<DataAttribute>> Delete([FromRoute] Guid id)
        {
            var result = _dataAttributeServices.Delete(id);
            var response = new BaseModel<DataAttribute>()
            {
                Data = result
            };
            return response;
        }

        [HttpPatch("[action]/{DataAttributeCode}")]
        public async Task<BaseModel<DataAttribute>> Update([FromBody] DataAttributeDto dataAttribute, [FromRoute] Guid id)
        {
            var result = _dataAttributeServices.Update(id, dataAttribute);
            var response = new BaseModel<DataAttribute>()
            {
                Data = result
            };
            return response;
        }
    }
}