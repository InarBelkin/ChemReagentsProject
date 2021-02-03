namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reagent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reagents", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reagents", "Number");
        }
    }
}
