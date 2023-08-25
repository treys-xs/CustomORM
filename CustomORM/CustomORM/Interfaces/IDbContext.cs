using System.Collections;

namespace CustomORM.Interfaces
{
    public interface IDbContext
    {
        TResult Map<TResult>(string sql, CancellationToken cancellationToken);
        string ResolveTableName(Type type);
    }
}
