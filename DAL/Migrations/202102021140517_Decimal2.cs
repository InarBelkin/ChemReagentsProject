namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Decimal2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplies", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplies", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
