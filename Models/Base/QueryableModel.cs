using Services.Models.Pagination;

namespace Services.Models.Base;

public class QueryableModel : PaginationQuery
{
    public string? Keyword { get; set; }
    public string? CreatedAt { get; set; }
    public string? Status { get; set; }

    public List<long> GetTimeRange<T>(string propertyName)
    {
        try
        {
            Type type = typeof(T);
            var prop = type.GetProperty(propertyName);
            var value = (string)prop.GetValue(this);

            return value.Split(":").Select(x => (long)Convert.ToDouble(x)).ToList();
        }
        catch (Exception ex)
        {
            return new List<long>();
        }
    }

    public List<long> CreatedAtRange()
    {
        if (string.IsNullOrEmpty(this.CreatedAt))
        {
            return new List<long>();
        }

        return this.CreatedAt.Split(":").Select(x => (long)Convert.ToDouble(x)).ToList();
    }

    //public IQueryable<TEntity> QueryByCreatedAt<TEntity>(IQueryable<TEntity> query) where TEntity : ITrackable
    //{
    //    var dateRange = CreatedAtRange();
    //    if (dateRange.Count == 0) return query;
    //    return query.Where(x => x.CreatedAt >= dateRange[0] && x.CreatedAt <= dateRange[1]);
    //}
}
