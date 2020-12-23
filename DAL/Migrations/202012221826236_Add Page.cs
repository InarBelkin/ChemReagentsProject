namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplies", "State", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplies", "State");
        }
    }
}
