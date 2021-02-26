namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Supplies", new[] { "SupplierId" });
            AlterColumn("dbo.Supplies", "SupplierId", c => c.Int());
            CreateIndex("dbo.Supplies", "SupplierId");
            AddForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Supplies", new[] { "SupplierId" });
            AlterColumn("dbo.Supplies", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Supplies", "SupplierId");
            AddForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
    }
}
