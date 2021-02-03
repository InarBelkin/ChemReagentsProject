namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplies", "Density", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            DropColumn("dbo.Reagents", "Density");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reagents", "Density", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            DropColumn("dbo.Supplies", "Density");
        }
    }
}
