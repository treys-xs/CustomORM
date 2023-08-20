using CustomORM.Interfaces;
using Npgsql;
using System.Data;

namespace CustomORM.DataBaseRealization.PostgreSQL
{
    public class PgsqlCommandAdapter : ICommand
    {
        private readonly NpgsqlCommand _command;
        public PgsqlCommandAdapter(NpgsqlCommand command) =>
            _command = command;

        public async Task<IDataReader> ExecuteReaderAsync()
        {
           return await _command.ExecuteReaderAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _command.DisposeAsync();
        }

    }
}
