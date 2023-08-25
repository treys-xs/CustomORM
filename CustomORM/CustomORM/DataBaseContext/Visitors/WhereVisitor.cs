using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CustomORM.DataBaseContext.Visitors
{
    public class WhereVisitor : BaseVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var operand = node.NodeType switch
            {
                ExpressionType.GreaterThan => ">",
                ExpressionType.LessThan => "<",
                ExpressionType.Equal => "=",
                ExpressionType.OrElse => "OR",
                ExpressionType.AndAlso => "AND"
            };

            Result = $"{ToString(node.Left)} {operand} {ToString(node.Right)}";

            return base.VisitBinary(node);
        }

    }
}
