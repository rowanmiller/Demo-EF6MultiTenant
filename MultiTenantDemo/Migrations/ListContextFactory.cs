using System.Data.Entity.Infrastructure;

namespace MultiTenantDemo.Migrations
{
    public class ListContextFactory : IDbContextFactory<ListContext>
    {
        public static readonly string DevelopmentSchema = "dev_schema";

        public ListContext Create()
        {
            return new ListContext(DevelopmentSchema);
        }
    }
}
