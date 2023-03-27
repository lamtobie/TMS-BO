using Databases.Entities;
using Services.Models.DataAttribute;
using Services.Models.Pagination;

namespace Services.Interfaces;

public interface IDataAttributeServices
{
    Task<PaginatedResultDto<DataAttribute>> GetAll(PaginationQuery query);
    DataAttribute Create(DataAttributeDto data);
    DataAttribute Delete(Guid id);
    DataAttribute Update(Guid id, DataAttributeDto data);
}