using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.MSSQL.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual)
                {
                    queryable = queryable.Include(property.Name);
                }
            }
            return queryable;
        }

        public static IQueryable<T> FilterWhen<T>(this IQueryable<T> queryable, bool codition, Func<T, bool> filter) where T : class
        {
            if (codition)
                queryable = queryable.Where(filter).AsQueryable();

            return queryable;
        }
    }
}
