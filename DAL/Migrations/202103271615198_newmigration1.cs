namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reagents", "Formula", c => c.String());
            AddColumn("dbo.Reagents", "Synonyms", c => c.String());
            AddColumn("dbo.Reagents", "GOST", c => c.String());
            AddColumn("dbo.Reagents", "Location", c => c.String());
            AddColumn("dbo.Supplies", "Manufacturer", c => c.String());
            AddColumn("dbo.Supplies", "IncomContr", c => c.String());
            AddColumn("dbo.Supplies", "DateIncomContr", c => c.DateTime(nullable: false));
            AddColumn("dbo.Supplies", "Qualification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplies", "Qualification");
            DropColumn("dbo.Supplies", "DateIncomContr");
            DropColumn("dbo.Supplies", "IncomContr");
            DropColumn("dbo.Supplies", "Manufacturer");
            DropColumn("dbo.Reagents", "Location");
            DropColumn("dbo.Reagents", "GOST");
            DropColumn("dbo.Reagents", "Synonyms");
            DropColumn("dbo.Reagents", "Formula");
        }
    }
}
