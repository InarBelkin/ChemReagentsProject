namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supply_consumption", "DateBegin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supply_consumption", "DateBegin");
        }
    }
}
