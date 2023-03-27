using Services.Models.Pagination;

namespace Services.Interfaces;

public interface ICommonServices
{
    PaginatedResultDto<T> CreatePaginationResponse<T>(IQueryable<T> queryable, PaginationQuery paginationQuery);
    PaginatedResultDto<TEntityDTO> CreatePaginationDtoResponse<TEntity, TEntityDTO>(IQueryable<TEntity> queryable, PaginationQuery paginationQuery);
}