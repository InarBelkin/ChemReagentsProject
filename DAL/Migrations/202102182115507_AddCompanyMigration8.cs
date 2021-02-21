namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solutions", "Count");
        }
    }
}
