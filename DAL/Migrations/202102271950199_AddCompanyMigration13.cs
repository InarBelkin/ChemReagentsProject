namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "RealDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "RealDate");
        }
    }
}
