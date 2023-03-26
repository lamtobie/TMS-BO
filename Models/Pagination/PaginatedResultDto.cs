using Services.Models.Base;

namespace Services.Models.Pagination;

public class PaginatedResultDto<T> : BaseModel<PaginatedDTO<T>>
{
}

public record PaginatedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
}
