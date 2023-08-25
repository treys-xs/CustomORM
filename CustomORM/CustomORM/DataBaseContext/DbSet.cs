using CustomORM.DataBaseContext.CustomQueryble;

namespace CustomORM.DataBaseContext
{
    public class DbSet<T> : DbQueryable<T>
    {
        public DbSet(IQueryProvider provider) : base(provider)
        {
        }
    }
}
