using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using Pms.Core.Expressions;

namespace Pms.Core.Extensions
{
    public static class ExpressionExtension
    {
        /// <summary>
        /// Appends an OrElse to the expression
        /// </summary>
        /// <param name="expression">Expression instance</param>
        /// <param name="expressionOr">Expression to be appended on the OrElse</param>
        public static Expression<Func<TEntity, bool>> OrElse<TEntity>(
            this Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, bool>> expressionOr)
        {
            if (expression == null) return expressionOr;

            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.OrElse(new ExpressionSwapper(expression.Parameters[0], expressionOr.Parameters[0])
                    .Visit(expression.Body), expressionOr.Body),
                expressionOr.Parameters);
        }

        /// <summary>
        /// Appends an AndAlso to the expression
        /// </summary>
        /// <param name="expression">Expression instance</param>
        /// <param name="expressionOr">Expression to be appended on the AndAlso</param>
        public static Expression<Func<TEntity, bool>> AndAlso<TEntity>(
            this Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, bool>> expressionAnd)
        {
            if (expression == null) return expressionAnd;

            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(new ExpressionSwapper(expression.Parameters[0], expressionAnd.Parameters[0])
                    .Visit(expression.Body), expressionAnd.Body),
                expressionAnd.Parameters);
        }

        /// <summary>
        /// Gets the property information
        /// </summary>
        /// <param name="propertyExpression">Property Expression</param>
        public static MemberInfo GetPropertyInformation(this Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }
            return null;
        }
    }
}
