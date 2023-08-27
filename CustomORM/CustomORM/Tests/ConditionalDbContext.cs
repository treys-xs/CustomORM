using CustomORM.DataBaseContext;
using CustomORM.Interfaces;

namespace CustomORM.Tests
{
    public class ConditionalDbContext : DbContext
    {
        public DbSet<ConditionalData> Сonditionals { get; set; }

        public ConditionalDbContext(IConnection connection) : base(connection)
        {
        }
    }
}
