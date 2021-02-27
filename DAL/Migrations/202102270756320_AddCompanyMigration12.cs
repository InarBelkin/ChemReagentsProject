namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeRep = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Supplies", "ReportId", c => c.Int());
            CreateIndex("dbo.Supplies", "ReportId");
            AddForeignKey("dbo.Supplies", "ReportId", "dbo.Reports", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplies", "ReportId", "dbo.Reports");
            DropIndex("dbo.Supplies", new[] { "ReportId" });
            DropColumn("dbo.Supplies", "ReportId");
            DropTable("dbo.Reports");
        }
    }
}
