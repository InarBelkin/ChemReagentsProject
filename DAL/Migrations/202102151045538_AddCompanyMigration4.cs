namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplies", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.Supplies", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Supplies", "State", c => c.Byte(nullable: false));
            DropColumn("dbo.Supplies", "Active");
        }
    }
}
