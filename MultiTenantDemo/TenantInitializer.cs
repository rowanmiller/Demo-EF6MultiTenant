using MultiTenantDemo.Migrations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.SqlServer;

namespace MultiTenantDemo
{
    public static class TenantInitializer
    {
        private static readonly string _schemaCreationSql = @"IF NOT EXISTS (SELECT 1 FROM information_schema.schemata WHERE schema_name = '{0}' )
                                                     BEGIN
                                                         EXEC sp_executesql N'CREATE SCHEMA [{0}];';
                                                     END";

        public static void EnsureTenantInitialized(string schema)
        {
            using (var db = new ListContext(schema))
            {
                // Ensure that the physical database exists (create an empty one if not)
                using (var b = new BlankContext(db.Database.Connection))
                {
                    if (!b.Database.Exists())
                    {
                        ((IObjectContextAdapter)b).ObjectContext.CreateDatabase();
                    }
                }

                // Ensure the schema exists (migrations won't create it since we are messing with object names)
                db.Database.ExecuteSqlCommand(string.Format(_schemaCreationSql, schema));

                // Apply migrations redirected to the schema
                var config = new Configuration();
                config.SetSqlGenerator(SqlProviderServices.ProviderInvariantName, new SchemaRenamingMigrationSqlGenerator(schema));
                config.SetHistoryContextFactory(SqlProviderServices.ProviderInvariantName, (c, s) => new HistoryContext(c, schema));
                new DbMigrator(config).Update();
            }
        }

        public class SchemaRenamingMigrationSqlGenerator : SqlServerMigrationSqlGenerator
        {
            private readonly string _newSchema;

            public SchemaRenamingMigrationSqlGenerator(string newSchema)
            {
                _newSchema = newSchema;
            }

            protected override string Name(string name)
            {
                name = name.Replace(ListContextFactory.DevelopmentSchema, _newSchema);
                return base.Name(name);
            }
        }

        private class BlankContext : DbContext
        {
            static BlankContext()
            {
                Database.SetInitializer<BlankContext>(null);
            }

            public BlankContext(DbConnection connection)
                : base(connection, contextOwnsConnection: false)
            { }
        }
    }
}
