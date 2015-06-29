namespace MultiTenantDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dev_schema.Lists", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dev_schema.Lists", "Description");
        }
    }
}
