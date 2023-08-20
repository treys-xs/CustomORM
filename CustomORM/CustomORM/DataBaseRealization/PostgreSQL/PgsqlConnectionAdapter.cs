using CustomORM.Interfaces;
using Npgsql;

namespace CustomORM.DataBaseRealization.PostgreSQL
{
    public class PgsqlConnectionAdapter : IConnection
    {
        private readonly NpgsqlConnection _connection;

        public PgsqlConnectionAdapter(string settings) =>
            _connection = new NpgsqlConnection(settings);

        public ICommand CreateCommand(string sql)
        {
            var command = _connection.CreateCommand();
            command.CommandText = sql;
            return new PgsqlCommandAdapter(command);
        }

        public async ValueTask DisposeAsync()
        {
            await _connection.DisposeAsync();
        }

        public async Task OpenAsync()
        {
            await _connection.OpenAsync();
        }
    }
}
