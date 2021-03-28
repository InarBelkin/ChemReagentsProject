namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solution_recipe", "GOST", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solution_recipe", "GOST");
        }
    }
}
