namespace MultiTenantDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListColumns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dev_schema.ListColumns",
                c => new
                    {
                        ListColumnId = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        ListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ListColumnId)
                .ForeignKey("dev_schema.Lists", t => t.ListId, cascadeDelete: true)
                .Index(t => t.ListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dev_schema.ListColumns", "ListId", "dev_schema.Lists");
            DropIndex("dev_schema.ListColumns", new[] { "ListId" });
            DropTable("dev_schema.ListColumns");
        }
    }
}
