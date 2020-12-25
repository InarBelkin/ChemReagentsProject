namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherComponentName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Solution_line", "SupplyId", "dbo.Supplies");
            DropIndex("dbo.Solution_line", new[] { "SupplyId" });
            AddColumn("dbo.Solution_line", "NameOtherComponent", c => c.String());
            AlterColumn("dbo.Solution_line", "SupplyId", c => c.Int());
            CreateIndex("dbo.Solution_line", "SupplyId");
            AddForeignKey("dbo.Solution_line", "SupplyId", "dbo.Supplies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solution_line", "SupplyId", "dbo.Supplies");
            DropIndex("dbo.Solution_line", new[] { "SupplyId" });
            AlterColumn("dbo.Solution_line", "SupplyId", c => c.Int(nullable: false));
            DropColumn("dbo.Solution_line", "NameOtherComponent");
            CreateIndex("dbo.Solution_line", "SupplyId");
            AddForeignKey("dbo.Solution_line", "SupplyId", "dbo.Supplies", "Id", cascadeDelete: true);
        }
    }
}
