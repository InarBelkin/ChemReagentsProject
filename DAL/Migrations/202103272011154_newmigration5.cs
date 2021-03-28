namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "CoefCorrect", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            DropColumn("dbo.Solutions", "CorrectFac");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solutions", "CorrectFac", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Solutions", "CoefCorrect");
        }
    }
}
