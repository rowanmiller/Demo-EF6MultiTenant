using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MultiTenantDemo
{
    public class ListContext : DbContext, IDbModelCacheKeyProvider
    {
        private readonly string _schema;
        private readonly string _cacheKey;

        public ListContext(string schema)
        {
            _schema = schema;
            _cacheKey = string.Format("{0}|{1}", typeof(ListContext).FullName, _schema);
        }

        string IDbModelCacheKeyProvider.CacheKey
        {
            get { return _cacheKey; }
        }

        public DbSet<List> Lists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);
        }
    }
}
