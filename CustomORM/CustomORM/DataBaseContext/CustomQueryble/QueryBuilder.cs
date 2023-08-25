using CustomORM.Interfaces;
using System.Linq.Expressions;
using CustomORM.DataBaseContext.Visitors;

namespace CustomORM.DataBaseContext.CustomQueryble
{
    public class QueryBuilder : ExpressionVisitor
    {
        private readonly IDbContext _context;
        private Expression _selectList, 
            _whereExpression;

        public QueryBuilder(IDbContext context)
        {
            _context = context;
        }


        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.IsGenericMethod)
            {
                var genericMethod = node.Method.GetGenericMethodDefinition();

                if (genericMethod == QueryableMethods.Select)
                    VisitSelect(node);
                else if (genericMethod == QueryableMethods.Where)
                    VisitWhere(node);
            }

            return base.VisitMethodCall(node);
        }

        public string Compile(Expression expression)
        {
            Visit(expression);
            var whereVisitor = new WhereVisitor();
            var selectVisitor = new SelectVisitor();
            whereVisitor.Visit(_whereExpression);
            selectVisitor.Visit(_selectList);
            var whereResult = whereVisitor.Result;
            var selectResult = selectVisitor.Result;
            var tableName = _context.ResolveTableName(expression.Type);
            var sql = $"""
             SELECT
                {selectResult}
             FROM
                {tableName}
             WHERE
                {whereResult}
             """;
            return sql;
        }

        private void VisitWhere(MethodCallExpression node)
        {
           _whereExpression = ((UnaryExpression)node.Arguments[1]).Operand;
        }

        private void VisitSelect(MethodCallExpression node)
        {
            _selectList = ((UnaryExpression)node.Arguments[1]).Operand;
        }


    }
}
