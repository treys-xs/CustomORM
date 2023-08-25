using System.Linq.Expressions;

namespace CustomORM.DataBaseContext.Visitors
{
    public class SelectVisitor : BaseVisitor
    {
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            var nodes = node.Bindings.Cast<MemberAssignment>().Select(x => ToString(x.Expression));

            Result = string.Join(',', nodes);

            return base.VisitMemberInit(node);
        }
    }
}
