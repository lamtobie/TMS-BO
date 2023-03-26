using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Services.Helper.Extensions
{
    public static class IQueryExtension
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, Dictionary<string, Expression<Func<T, object>>> columnsMap, string sortBy, string? sortType = "asc")
        {
            if (string.IsNullOrWhiteSpace(sortBy) || !columnsMap.ContainsKey(sortBy))
                return query;
            if (string.IsNullOrEmpty(sortType))
            {
                sortType = "asc";
            }
            return sortType == "asc" ? query.OrderBy(columnsMap[sortBy]) : query.OrderByDescending(columnsMap[sortBy]);
        }
    }
}
