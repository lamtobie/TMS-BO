using System.Text.Json.Serialization;

namespace Services.Models.Pagination;

public class PaginatedDTO<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; }

    [JsonPropertyName("count_per_page")]
    public int CountPerPage { get; set; } = 20;

    [JsonPropertyName("total_page")]
    public int TotalPage { get; set; }
    
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
}