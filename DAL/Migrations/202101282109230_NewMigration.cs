namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reagents", "isWater", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reagents", "density", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Reagents", "units");
            DropColumn("dbo.Reagents", "CostUnit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reagents", "CostUnit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Reagents", "units", c => c.String());
            DropColumn("dbo.Reagents", "density");
            DropColumn("dbo.Reagents", "isWater");
        }
    }
}
