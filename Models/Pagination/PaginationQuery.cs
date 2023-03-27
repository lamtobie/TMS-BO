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

    public int PageSize { get; set; }

    public int Page { get; set; }
}
