namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "GOST", c => c.String());
            AddColumn("dbo.Solutions", "CorrectFac", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solutions", "CorrectFac");
            DropColumn("dbo.Solutions", "GOST");
        }
    }
}
