namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Supplies", "Unpacked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Supplies", "Unpacked", c => c.Boolean(nullable: false));
        }
    }
}
