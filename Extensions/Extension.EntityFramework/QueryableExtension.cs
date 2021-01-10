using System.Linq;

namespace Extension.EntityFramework
{
    public static class QueryableExtension
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> queryable, int pageIndex, int pageSize)
        {
            return queryable.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
    }
}