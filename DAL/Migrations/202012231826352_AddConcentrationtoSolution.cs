namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConcentrationtoSolution : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Solutions", "SolutionRecipeId", "dbo.Solution_recipe");
            DropIndex("dbo.Solutions", new[] { "SolutionRecipeId" });
            AddColumn("dbo.Solutions", "ConcentrationId", c => c.Int());
            CreateIndex("dbo.Solutions", "ConcentrationId");
            AddForeignKey("dbo.Solutions", "ConcentrationId", "dbo.Concentrations", "Id");
            DropColumn("dbo.Solutions", "SolutionRecipeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solutions", "SolutionRecipeId", c => c.Int());
            DropForeignKey("dbo.Solutions", "ConcentrationId", "dbo.Concentrations");
            DropIndex("dbo.Solutions", new[] { "ConcentrationId" });
            DropColumn("dbo.Solutions", "ConcentrationId");
            CreateIndex("dbo.Solutions", "SolutionRecipeId");
            AddForeignKey("dbo.Solutions", "SolutionRecipeId", "dbo.Solution_recipe", "Id");
        }
    }
}
