namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplies", "Date_StartUse", c => c.DateTime(nullable: false));
            DropColumn("dbo.Supplies", "Date_Begin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Supplies", "Date_Begin", c => c.DateTime(nullable: false));
            DropColumn("dbo.Supplies", "Date_StartUse");
        }
    }
}
