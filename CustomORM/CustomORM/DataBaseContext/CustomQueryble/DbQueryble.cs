using System.Collections;
using System.Linq.Expressions;

namespace CustomORM.DataBaseContext.CustomQueryble
{
    public class DbQueryable<T> : IQueryable<T>
    {
        public Type ElementType { get; }

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public DbQueryable(IQueryProvider provider)
        {
            ElementType = typeof(T);
            Expression = Expression.Constant(this);
            Provider = provider;
        }
        public DbQueryable(Expression expression, IQueryProvider provider)
        {
            ElementType = typeof(T);
            Expression = expression;
            Provider = provider;

        }

        public IEnumerator<T> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
