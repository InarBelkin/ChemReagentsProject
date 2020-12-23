namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Solutions", "SolutionRecipeId", "dbo.Solution_recipe");
            DropIndex("dbo.Solutions", new[] { "SolutionRecipeId" });
            AlterColumn("dbo.Solutions", "SolutionRecipeId", c => c.Int());
            CreateIndex("dbo.Solutions", "SolutionRecipeId");
            AddForeignKey("dbo.Solutions", "SolutionRecipeId", "dbo.Solution_recipe", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solutions", "SolutionRecipeId", "dbo.Solution_recipe");
            DropIndex("dbo.Solutions", new[] { "SolutionRecipeId" });
            AlterColumn("dbo.Solutions", "SolutionRecipeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Solutions", "SolutionRecipeId");
            AddForeignKey("dbo.Solutions", "SolutionRecipeId", "dbo.Solution_recipe", "Id", cascadeDelete: true);
        }
    }
}
