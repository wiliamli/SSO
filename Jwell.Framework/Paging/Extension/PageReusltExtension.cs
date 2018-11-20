using System.Collections.Generic;
using System.Linq;

namespace Jwell.Framework.Paging
{
    public static class PageReusltExtension
    {
        public static PageResult<T> ToPageResult<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PageResult<T>(source, pageIndex, pageSize, source.Count());
        }

        public static PageResult<T> ToPageResult<T>(this IQueryable<T> linq, int pageIndex, int pageSize)
        {
            return new PageResult<T>(linq, pageIndex, pageSize);
        }

        public static PageResult<T> ToPageResult<T>(this IQueryable<T> query, PageParam request)
        {
            return new PageResult<T>(query.OrderBy(request.Sort, request.SortDirection), request.PageIndex, request.PageSize);
        }
    }
}
