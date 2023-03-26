using Microsoft.AspNetCore.Mvc;

namespace Services.Models.Pagination;

public class PaginationQuery
{
    public PaginationQuery()
    {
        this.PageSize = 10;
        this.Page = 1;
    }

    public PaginationQuery(int pageSize, int page)
    {
        this.PageSize = pageSize;
        this.Page = Page;
    }

    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; }

    [FromQuery(Name = "page")]
    public int Page { get; set; }
}
