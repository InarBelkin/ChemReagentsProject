namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "Date_End", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solutions", "Date_End");
        }
    }
}
