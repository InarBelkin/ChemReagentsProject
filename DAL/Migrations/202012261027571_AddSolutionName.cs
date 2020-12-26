namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSolutionName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "SolutName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solutions", "SolutName");
        }
    }
}
