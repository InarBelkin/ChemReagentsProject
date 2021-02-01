namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplies", "Unpacked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplies", "Unpacked");
        }
    }
}
