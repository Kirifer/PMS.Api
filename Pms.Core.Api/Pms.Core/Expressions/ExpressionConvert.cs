using System.Linq.Expressions;

namespace Pms.Core.Expressions
{
    public static class ExpressionConvert
    {
        /// <summary>
        /// Sorts the queryable records.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be sorted.</typeparam>
        /// <param name="query">The queryable records.</param>
        /// <param name="methodName">The method name to be used for sorting.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="comparer">The comparison object for comparison.</param>
        /// <returns>The ordered queryable items.</returns>
        public static IOrderedQueryable<TEntity> ToOrderedQueryable<TEntity>(
            IQueryable<TEntity> query, string methodName, string propertyName,
            IComparer<object> comparer = null)
        {
            var param = Expression.Parameter(typeof(TEntity), "x");

            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return comparer != null
                ? (IOrderedQueryable<TEntity>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(TEntity), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param),
                        Expression.Constant(comparer)
                    )
                )
                : (IOrderedQueryable<TEntity>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(TEntity), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param)
                    )
                );
        }
    }
}
