namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Supplies", "DateIncomContr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Supplies", "DateIncomContr", c => c.DateTime(nullable: false));
        }
    }
}
