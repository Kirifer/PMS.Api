using System.Linq.Expressions;

namespace Pms.Core.Expressions
{
    public class ExpressionSwapper : ExpressionVisitor
    {
        private readonly Expression _from;
        private readonly Expression _to;

        public ExpressionSwapper(Expression from, Expression to)
        {
            _from = from;
            _to = to;
        }

        public override Expression Visit(Expression node)
        {
            return node == _from ? _to : base.Visit(node);
        }
    }
}
