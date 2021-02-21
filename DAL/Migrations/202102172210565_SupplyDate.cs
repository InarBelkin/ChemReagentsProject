namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplyDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplies", "Date_Production", c => c.DateTime(nullable: false));
            AddColumn("dbo.Supplies", "Date_Expiration", c => c.DateTime(nullable: false));
            AddColumn("dbo.Supplies", "Date_UnWrite", c => c.DateTime(nullable: false));
            DropColumn("dbo.Supplies", "Date_End");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Supplies", "Date_End", c => c.DateTime(nullable: false));
            DropColumn("dbo.Supplies", "Date_UnWrite");
            DropColumn("dbo.Supplies", "Date_Expiration");
            DropColumn("dbo.Supplies", "Date_Production");
        }
    }
}
