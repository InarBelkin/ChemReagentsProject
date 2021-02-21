namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "RecipeId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solutions", "RecipeId");
        }
    }
}
