using System.Linq.Expressions;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Pms.Core.Expressions;
using Pms.Shared.Extensions;

namespace Pms.Core.Extensions
{
    public static class QueryableExtension
    {
        /// <summary>
        /// Compares the keyword against the property using LIKE operator in SQL command
        /// </summary>
        /// <param name="source">Queryable source items</param>
        /// <param name="properties">Properties to add on expression builder</param>
        public static IQueryable<TEntity> WherePropertyLike<TEntity>(
            this IQueryable<TEntity> source,
            params (string keyword, PropertyInfo propertyInfo)[] properties)
        {
            if (source == null) { return source; }

            Expression<Func<TEntity, bool>> mergedExpressions = default;
            var parameterExpression = Expression.Parameter(typeof(TEntity), "entity");
            var likeMethodInfo = typeof(NpgsqlDbFunctionsExtensions).GetMethod("ILike",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(DbFunctions), typeof(string), typeof(string) },
                null);

            foreach (var property in properties)
            {
                if (string.IsNullOrWhiteSpace(property.keyword)) continue;

                var propertyInfo = property.propertyInfo;
                if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
                {
                    throw new Exception(propertyInfo.Name);
                }
                var propertyExpression = Expression.Property(parameterExpression, propertyInfo.Name);
                var constantExpression = Expression.Constant($"%{property.keyword ?? string.Empty}%", typeof(string));

                var likeExpression = Expression.Call(
                    likeMethodInfo,
                    Expression.Property(null, typeof(EF), nameof(EF.Functions)),
                    propertyExpression,
                    constantExpression);

                var lambdaExpression = Expression.Lambda<Func<TEntity, bool>>(likeExpression, parameterExpression);

                mergedExpressions = Equals(mergedExpressions, null) ?
                    lambdaExpression : mergedExpressions.OrElse(lambdaExpression);
            }

            return Equals(mergedExpressions, null) ?
                source :
                source.Where(mergedExpressions);
        }

        /// <summary>
        /// Compares the keyword against the property using LIKE operator in SQL command when condition meets
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">Queryable source items</param>
        /// <param name="predicateCondition">Predicate condition to be checked</param>
        /// <param name="properties">Properties to add on expression builder</param>
        /// <returns></returns>
        public static IQueryable<TEntity> ConditionalWhereContains<TEntity>(
            this IQueryable<TEntity> source,
            Func<bool> predicateCondition,
            params (string keyword, Expression<Func<TEntity, string>> nameSelector)[] properties)
        {
            if (!predicateCondition()) { return source; }
            return source.WherePropertyContains(properties);
        }

        /// <summary>
        /// Compares the keyword against the property using LIKE operator in SQL command when condition meets
        /// with keyword being split by Space
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">Queryable source items</param>
        /// <param name="properties">Properties to add on expression builder</param>
        /// <returns></returns>
        public static IQueryable<TEntity> ConditionalWhereContains<TEntity>(
            this IQueryable<TEntity> source,
            params (Func<bool> predicateCondition, string wordToSplitBySpace,
                Expression<Func<TEntity, string>> nameSelector)[] properties)
        {
            var splittedProperty = new List<(string keyword, Expression<Func<TEntity, string>> nameSelector)>();

            var activeProperties = properties.Where(item => !string.IsNullOrEmpty(item.wordToSplitBySpace) && item.predicateCondition());
            foreach (var (predicateCondition, wordToSplitBySpace, nameSelector) in activeProperties)
            {
                wordToSplitBySpace.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(splitKeyword =>
                        splittedProperty.Add(new(splitKeyword, nameSelector)));
            }
            return source.WherePropertyContains(splittedProperty.ToArray());
        }

        /// <summary>
        /// Compares the keyword against the property using LIKE operator in SQL command
        /// </summary>
        /// <param name="source">Queryable source items</param>
        /// <param name="properties">Properties to add on expression builder</param>
        public static IQueryable<TEntity> WherePropertyContains<TEntity>(
            this IQueryable<TEntity> source,
            params (string keyword, Expression<Func<TEntity, string>> nameSelector)[] properties)
        {
            if (source == null) { return source; }

            Func<Expression<Func<TEntity, string>>, PropertyInfo> convertToProperty =
                delegate (Expression<Func<TEntity, string>> selector)
                {
                    var propertyBody = ((LambdaExpression)selector).Body;
                    return (PropertyInfo)((MemberExpression)propertyBody).Member;
                };

            var filteredProperties = properties
                .Where(keyword => !keyword.keyword.IsNullOrEmpty())
                .Select(property => (property.keyword, convertToProperty(property.nameSelector)))
                .ToArray();

            return source.WherePropertyLike(filteredProperties);
        }

        /// <summary>
        /// Sorts the queryable records in ascending order.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The queryable item to be sorted.</param>
        /// <param name="propertyName">The property name to be used.</param>
        /// <param name="comparer">The comparare function.</param>
        /// <returns>Sorted queryable records.</returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query,
            string propertyName, IComparer<object> comparer = null)
        {
            return ExpressionConvert.ToOrderedQueryable(query, "OrderBy", propertyName, comparer);
        }

        /// <summary>
        /// Sorts the queryable records in descending order.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The queryable item to be sorted.</param>
        /// <param name="propertyName">The property name to be used.</param>
        /// <param name="comparer">The comparare function.</param>
        /// <returns>Sorted queryable records.</returns>
        public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> query,
            string propertyName, IComparer<object> comparer = null)
        {
            return ExpressionConvert.ToOrderedQueryable(query, "OrderByDescending", propertyName, comparer);
        }

        /// <summary>
        /// Sorts the queryable records in ascending order next to the OrderBy Clause.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The queryable item to be sorted.</param>
        /// <param name="propertyName">The property name to be used.</param>
        /// <param name="comparer">The comparare function.</param>
        /// <returns>Sorted queryable records.</returns>
        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> query,
            string propertyName, IComparer<object> comparer = null)
        {
            return ExpressionConvert.ToOrderedQueryable(query, "ThenBy", propertyName, comparer);
        }

        /// <summary>
        /// Sorts the queryable records in ascending order next to the OrderByDescending Clause.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The queryable item to be sorted.</param>
        /// <param name="propertyName">The property name to be used.</param>
        /// <param name="comparer">The comparare function.</param>
        /// <returns>Sorted queryable records.</returns>
        public static IOrderedQueryable<TEntity> ThenByDescending<TEntity>(this IOrderedQueryable<TEntity> query,
            string propertyName, IComparer<object> comparer = null)
        {
            return ExpressionConvert.ToOrderedQueryable(query, "ThenByDescending", propertyName, comparer);
        }

        /// <summary>
        /// Adds the provided clause when the predicate condition is true
        /// </summary>
        /// <param name="query">Query where to append the where clause</param>
        /// <param name="predicateCondition">Predicate condition to be checked</param>
        /// <param name="clause">Where Clause to be appended</param>
        public static IQueryable<TEntity> ConditionalWhere<TEntity>(
            this IQueryable<TEntity> query,
            Func<bool> predicateCondition,
            Expression<Func<TEntity, bool>> clause)
        {
            return predicateCondition() ? query.Where(clause) : query;
        }

        /// <summary>
        /// Adds the provided include clause when the predicate condition is true
        /// </summary>
        /// <param name="query">Query where to append the where clause</param>
        /// <param name="predicateCondition">Predicate condition to be checked</param>
        /// <param name="includeClause">Include Clause to be appended</param>
        public static IQueryable<TEntity> ConditionalInclude<TEntity, TProperty>(
            this IQueryable<TEntity> query,
            Func<bool> predicateCondition,
            Expression<Func<TEntity, TProperty>> includeClause)
            where TEntity : class
        {
            if (!predicateCondition()) { return query; }
            return query.Include(includeClause);
        }

        /// <summary>
        /// Adds the provided single include clause when the predicate condition is true
        /// together with its thenInclude clause
        /// </summary>
        /// <param name="query">Query where to append the where clause</param>
        /// <param name="predicateCondition">Predicate condition to be checked</param>
        /// <param name="includeClause">Include Clause to be appended</param>
        /// <param name="thenClause">Then Include Clause to be appended</param>
        public static IQueryable<TEntity> ConditionalInclude<TEntity, TPreviousProperty, TProperty>(
            this IQueryable<TEntity> query,
            Func<bool> predicateCondition,
            Expression<Func<TEntity, TPreviousProperty>> includeClause,
            Expression<Func<TPreviousProperty, TProperty>> thenClause)
            where TEntity : class
        {
            if (!predicateCondition()) { return query; }
            return query.Include(includeClause).ThenInclude(thenClause);
        }

        /// <summary>
        /// Adds the provided enumerable include clause when the predicate condition is true
        /// together with its thenInclude clauses
        /// </summary>
        /// <param name="query">Query where to append the where clause</param>
        /// <param name="predicateCondition">Predicate condition to be checked</param>
        /// <param name="includeClause">Enumerable Include Clause to be appended</param>
        /// <param name="firstThenClause">First then include clause to be added</param>
        /// <param name="secondThenClause">Second then include clause to be added</param>
        public static IQueryable<TEntity> ConditionalInclude<TEntity, TPreviousProperty, TProperty, TNextProperty>(
            this IQueryable<TEntity> query,
            Func<bool> predicateCondition,
            Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> includeClause,
            Expression<Func<TPreviousProperty, TProperty>> firstThenClause,
            Expression<Func<TProperty, TNextProperty>> secondThenClause)
            where TEntity : class
        {
            if (!predicateCondition()) { return query; }
            return query.Include(includeClause)
                .ThenInclude(firstThenClause)
                .ThenInclude(secondThenClause);
        }

        public static IQueryable<TEntity> ConditionalInclude<TEntity, TPreviousProperty, TProperty, TNextProperty>(
            this IQueryable<TEntity> query,
            Func<bool> predicateCondition,
            Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> includeClause,
            Expression<Func<TPreviousProperty, IEnumerable<TProperty>>> firstThenClause)
            where TEntity : class
        {
            if (!predicateCondition()) { return query; }
            return query.Include(includeClause)
                .ThenInclude(firstThenClause);
        }
    }
}
