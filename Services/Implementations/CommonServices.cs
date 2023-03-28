using AutoMapper;
using Services.Interfaces;
using Services.Models.Pagination;

namespace Services.Implementations;

public class CommonServices : ICommonServices
{
    private readonly IMapper _mapper;

    public CommonServices(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Create pagination response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="paginationQuery"></param>
    /// <returns></returns>
    public PaginatedResultDto<T> CreatePaginationResponse<T>(IQueryable<T> queryable, PaginationQuery paginationQuery)
    {
        var totalCount = queryable.Count();
        var totalPage = paginationQuery.PageSize != 0 ? ((double)totalCount / (double)paginationQuery.PageSize) : 0;
        int roundedTotalPage = Convert.ToInt32(Math.Ceiling(totalPage));
        var skip = paginationQuery.Page > 0 ? (paginationQuery.Page - 1) * paginationQuery.PageSize : 0;
        var items = queryable.Skip(skip).Take(paginationQuery.PageSize).ToList();

        return new PaginatedResultDto<T>()
        {
            Data = new PaginatedDTO<T>()
            {
                Items = items,
                CountPerPage = items.Count(),
                TotalCount = totalCount,
                TotalPage = roundedTotalPage
            }
        };
    }

    public PaginatedResultDto<TEntityDTO> CreatePaginationDtoResponse<TEntity, TEntityDTO>(IQueryable<TEntity> queryable, PaginationQuery paginationQuery)
    {
        var result = CreatePaginationResponse<TEntity>(queryable, paginationQuery);
        var items = _mapper.Map<List<TEntity>, List<TEntityDTO>>(result.Data.Items);

        return new PaginatedResultDto<TEntityDTO>()
        {
            Data = new PaginatedDTO<TEntityDTO>()
            {
                Items = items,
                CountPerPage = items.Count(),
                TotalCount = result.Data.TotalCount,
                TotalPage = result.Data.TotalPage
            }
        };
    }
}