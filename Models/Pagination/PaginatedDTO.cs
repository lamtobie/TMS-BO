using System.Text.Json.Serialization;

namespace Services.Models.Pagination;

public class PaginatedDTO<T>
{
    public List<T> Items { get; set; }

    public int CountPerPage { get; set; } = 20;

    public int TotalPage { get; set; }
    
    public int TotalCount { get; set; }
}