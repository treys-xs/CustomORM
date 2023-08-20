using CustomORM.Interfaces;
using Npgsql;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomORM
{
    public class SqlMapper
    {
        private readonly MethodInfo
            GetStringMethod = typeof(DataReaderExtensions).GetMethod("GetString")!,
            GetInt32Method = typeof(DataReaderExtensions).GetMethod("GetInt32")!;

        public async Task<List<T>> MapAsync<T>(IConnection connection, string sql, CancellationToken cancellationToken)
        {
            await using var command = connection.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();

            var listEntity = new List<T>();
            Func<IDataReader, T> mapper = BuildMapper<T>();

            while (reader.Read())
            {
                listEntity.Add(mapper(reader));
            }

            return listEntity;
        }

        private Func<IDataReader, T> BuildMapper<T>()
        {
            var readerParam = Expression.Parameter(typeof(IDataReader));

            var newObject = Expression.New(typeof(T));
            var body = Expression.MemberInit(newObject, typeof(T).GetProperties()
                .Select(prop => Expression.Bind(prop, SetProperties(readerParam, prop))));

            return Expression.Lambda<Func<IDataReader, T>>(body, readerParam).Compile();

        }

        private Expression SetProperties(Expression reader, PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(string))
                return Expression.Call(null, GetStringMethod, reader, Expression.Constant(prop.Name));
            else if (prop.PropertyType == typeof(int))
                return Expression.Call(null, GetInt32Method, reader, Expression.Constant(prop.Name));
            throw new Exception();

        }
    }
}
