using AutoMapper;
using Databases.Entities;
using Databases.Interfaces;
using Repositories.DataAttributeRepository;
using Services.Interfaces;
using Services.Models.DataAttribute;
using Services.Models.Pagination;

namespace Services.Implementations;

public class DataAttributeServices : IDataAttributeServices
{
    private readonly ICommonServices _commonServices;
    private readonly IDataAttributeRepositories _dataAttributeRepositories;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DataAttributeServices(
        ICommonServices commonServices,
        IDataAttributeRepositories dataAttributeRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _dataAttributeRepositories = dataAttributeRepositories;
        _mapper = mapper;
    }


    public DataAttribute Create(DataAttributeDto dataAttributeDto)
    {
        var dataAttribute = _mapper.Map<DataAttributeDto, DataAttribute>(dataAttributeDto);
        _dataAttributeRepositories.Create(dataAttribute);
        return dataAttribute;
    }

    public async Task<PaginatedResultDto<DataAttribute>> GetAll(PaginationQuery query)
    {
        var queryable = _dataAttributeRepositories.GetAll();
        var result = _commonServices.CreatePaginationResponse<DataAttribute>(queryable, query);
        return result;
    }

    public DataAttribute Update(Guid id, DataAttributeDto dataAttributeDto)
    {
        var newDataAttribute = _mapper.Map<DataAttributeDto, DataAttribute>(dataAttributeDto);
        newDataAttribute.Id = id;
        var result = _dataAttributeRepositories.UpdateDataAttribute(newDataAttribute);
        return result;
    }

    public DataAttribute Delete(Guid id)
    {
        return _dataAttributeRepositories.DeleteDataAttribute(id);
    }
}