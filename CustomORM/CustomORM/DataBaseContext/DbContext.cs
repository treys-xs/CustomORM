using CustomORM.Interfaces;
using CustomORM.DataBaseContext.CustomQueryble;

namespace CustomORM.DataBaseContext
{

    public class DbContext : IDbContext
    {
        private readonly IConnection _connection;
        public DbContext(IConnection connection) 
        {
            _connection = connection;

            var sets = GetType().GetProperties()
                .Where(x => x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var set in sets)
            {
                set.SetValue(this, CreateSet(set.PropertyType.GetGenericArguments()[0]));
            }
        }

        public object? CreateSet(Type propertyType)
        {
            return typeof(DbContext).GetMethod("CreateSetGeneric")
                .MakeGenericMethod(propertyType)
                .Invoke(this, Array.Empty<object>());
        }

        public DbSet<T> CreateSetGeneric<T>() => new DbSet<T>(new DbQueryProvider(this));

        public TResult Map<TResult>(string sql, CancellationToken cancellationToken)
        {
            if(typeof(TResult).IsGenericType && typeof(TResult).GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var typeEntity = typeof(TResult).GetGenericArguments()[0];
                var mapper = new SqlMapper();
                var task = (Task)mapper.MapAsyncType(_connection, typeEntity, sql, default);
                task.Wait();
                dynamic result = task;
                return result.Result;
            }
            throw new NotImplementedException();
        }

        public string ResolveTableName(Type type)
        {
            var typeName = $"{type.GenericTypeArguments.Single().Name}s";
            return $"\"{GetType().GetProperties().FirstOrDefault(x => x.Name == typeName)?.Name}\"";
        }
    }
}