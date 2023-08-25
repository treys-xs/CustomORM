using CustomORM.DataBaseContext;
using CustomORM.Interfaces;

namespace CustomORM
{
    public class TestDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public TestDbContext(IConnection connection) : base(connection) 
        {
        }
    }
}
