namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Solutions", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Solutions", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
