using CustomORM.Interfaces;
using System.Linq.Expressions;

namespace CustomORM.DataBaseContext.CustomQueryble
{
    public class DbQueryProvider : IQueryProvider
    {
        private readonly IDbContext _context;

        public DbQueryProvider(IDbContext context) =>
            _context = context;
        public IQueryable CreateQuery(Expression expression) =>
            CreateQuery<object>(expression);

        public object? Execute(Expression expression) => 
            Execute<object>(expression);

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new DbQueryable<TElement>(expression, this);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var builder = new QueryBuilder(_context);
            var sql = builder.Compile(expression);

            return _context.Map<TResult>(sql, default);
        }
    }
}
