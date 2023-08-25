using System.Linq.Expressions;

namespace CustomORM.DataBaseContext.Visitors
{
    public abstract class BaseVisitor : ExpressionVisitor
    {
        public string Result { get; protected set; }

        protected string ToString(Expression expression)
        {
            if (expression is ConstantExpression cs)
                return cs.Value.ToString();
            return $"\"{((MemberExpression)expression).Member.Name}\"";
        }
    }
}
