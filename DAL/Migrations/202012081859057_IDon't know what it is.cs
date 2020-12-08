namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDontknowwhatitis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reagents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        units = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReagentId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        Date_Begin = c.DateTime(nullable: false),
                        Date_End = c.DateTime(nullable: false),
                        count = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reagents", t => t.ReagentId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.ReagentId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Solution_line",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SolutionId = c.Int(nullable: false),
                        SupplyId = c.Int(nullable: false),
                        Count = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Solutions", t => t.SolutionId, cascadeDelete: true)
                .ForeignKey("dbo.Supplies", t => t.SupplyId, cascadeDelete: true)
                .Index(t => t.SolutionId)
                .Index(t => t.SupplyId);
            
            CreateTable(
                "dbo.Solutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SolutionRecipeId = c.Int(nullable: false),
                        Date_Begin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Solution_recipe", t => t.SolutionRecipeId, cascadeDelete: true)
                .Index(t => t.SolutionRecipeId);
            
            CreateTable(
                "dbo.Solution_recipe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Solution_recipe_line",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReagentId = c.Int(nullable: false),
                        SolutionRecipeId = c.Int(nullable: false),
                        Count = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reagents", t => t.ReagentId, cascadeDelete: true)
                .ForeignKey("dbo.Solution_recipe", t => t.SolutionRecipeId, cascadeDelete: true)
                .Index(t => t.ReagentId)
                .Index(t => t.SolutionRecipeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solution_recipe_line", "SolutionRecipeId", "dbo.Solution_recipe");
            DropForeignKey("dbo.Solution_recipe_line", "ReagentId", "dbo.Reagents");
            DropForeignKey("dbo.Solution_line", "SupplyId", "dbo.Supplies");
            DropForeignKey("dbo.Solution_line", "SolutionId", "dbo.Solutions");
            DropForeignKey("dbo.Solutions", "SolutionRecipeId", "dbo.Solution_recipe");
            DropForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Supplies", "ReagentId", "dbo.Reagents");
            DropIndex("dbo.Solution_recipe_line", new[] { "SolutionRecipeId" });
            DropIndex("dbo.Solution_recipe_line", new[] { "ReagentId" });
            DropIndex("dbo.Solutions", new[] { "SolutionRecipeId" });
            DropIndex("dbo.Solution_line", new[] { "SupplyId" });
            DropIndex("dbo.Solution_line", new[] { "SolutionId" });
            DropIndex("dbo.Supplies", new[] { "SupplierId" });
            DropIndex("dbo.Supplies", new[] { "ReagentId" });
            DropTable("dbo.Solution_recipe_line");
            DropTable("dbo.Solution_recipe");
            DropTable("dbo.Solutions");
            DropTable("dbo.Solution_line");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Supplies");
            DropTable("dbo.Reagents");
        }
    }
}
