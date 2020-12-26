namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcentrationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "RecipeName", c => c.String());
            AddColumn("dbo.Solutions", "ConcentrName", c => c.String());
            DropColumn("dbo.Solutions", "SolutName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solutions", "SolutName", c => c.String());
            DropColumn("dbo.Solutions", "ConcentrName");
            DropColumn("dbo.Solutions", "RecipeName");
        }
    }
}
