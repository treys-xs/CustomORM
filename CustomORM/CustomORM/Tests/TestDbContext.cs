using CustomORM.DataBaseRealization.PostgreSQL;
using Xunit;

namespace CustomORM.Tests
{
    public class TestDbContext
    {

        [Fact]
        public async void DataEqualAsync()
        {
            var connectionString = String.Empty;
            await using var connection = new PgsqlConnectionAdapter(connectionString);
            await connection.OpenAsync();
            var context = new ConditionalDbContext(connection);

            var result = context.Сonditionals.Where(x => x.Id > 1).Select(x => new ConditionalData { Id = x.Id, Text = x.Text });
            var results = result.ToList();

            Assert.Equal(new List<ConditionalData>() { }, result);
        }
    }
}
