namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConsumptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supply_consumption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplyId = c.Int(nullable: false),
                        Name = c.String(),
                        Count = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supplies", t => t.SupplyId, cascadeDelete: true)
                .Index(t => t.SupplyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supply_consumption", "SupplyId", "dbo.Supplies");
            DropIndex("dbo.Supply_consumption", new[] { "SupplyId" });
            DropTable("dbo.Supply_consumption");
        }
    }
}
